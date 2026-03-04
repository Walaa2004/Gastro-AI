using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using ServicesAbstraction;
using Shared.DTO.AIResult;

namespace Services
{
    public class AIResultService : IAIResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AIResultService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AIResultDto>> GetAllResultsAsync()
        {
            var results = await _unitOfWork.AIResults.GetAllAsync();
            return _mapper.Map<IEnumerable<AIResultDto>>(results);
        }

        public async Task<IEnumerable<AIResultDto>> GetResultsByPatientIdAsync(int patientId)
        {
            var results = await _unitOfWork.AIResults.GetAllAsync();
            var patientResults = results.Where(r => r.PatientId == patientId).OrderByDescending(r => r.AnalyzedAt);
            return _mapper.Map<IEnumerable<AIResultDto>>(patientResults);
        }

        public async Task<IEnumerable<AIResultDto>> GetResultsByDoctorIdAsync(int doctorId)
        {
            var results = await _unitOfWork.AIResults.GetAllAsync();
            var doctorResults = results.Where(r => r.DoctorId == doctorId).OrderByDescending(r => r.AnalyzedAt);
            return _mapper.Map<IEnumerable<AIResultDto>>(doctorResults);
        }

        public async Task<IEnumerable<AIResultDto>> GetUnreviewedResultsAsync()
        {
            var results = await _unitOfWork.AIResults.GetAllAsync();
            var unreviewedResults = results.Where(r => !r.IsReviewedByDoctor).OrderBy(r => r.AnalyzedAt);
            return _mapper.Map<IEnumerable<AIResultDto>>(unreviewedResults);
        }

        public async Task<AIResultDto?> GetResultByIdAsync(int resultId)
        {
            var result = await _unitOfWork.AIResults.GetByIdAsync(resultId);

            if (result == null)
                return null;

            return _mapper.Map<AIResultDto>(result);
        }

        public async Task<AIResultDto> UploadAndAnalyzeImageAsync(int patientId, IFormFile image)
        {
            // Validate patient exists
            var patient = await _unitOfWork.Patients.GetByIdAsync(patientId);
            if (patient == null)
                throw new Exception("Patient not found");

            // Validate image
            if (image == null || image.Length == 0)
                throw new Exception("Invalid image file");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new Exception("Only JPG, JPEG, and PNG files are allowed");

            // Save image to wwwroot/uploads
            var uploadsFolder = Path.Combine("wwwroot", "uploads", "ai-images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/ai-images/{uniqueFileName}";

            // TODO: في المستقبل هنا هيتم استدعاء Python AI Model
            // دلوقتي هنحط بيانات Placeholder

            var aiResult = new AIResult
            {
                PatientId = patientId,
                ModelId = 1, // Default AI Model
                ImagePath = imageUrl,
                DiseaseName = "Pending Analysis", // Placeholder
                Description = "Image uploaded successfully. AI analysis will be performed when the model is integrated.",
                Confidence = 0,
                Recommendations = "Please consult with a doctor for professional diagnosis.",
                AnalyzedAt = DateTime.UtcNow,
                IsReviewedByDoctor = false
            };

            await _unitOfWork.AIResults.AddAsync(aiResult);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AIResultDto>(aiResult);
        }

        public async Task<AIResultDto?> ReviewResultAsync(ReviewAIResultDto reviewDto)
        {
            // Get existing result
            var result = await _unitOfWork.AIResults.GetByIdAsync(reviewDto.ResultId);
            if (result == null)
                return null;

            // Validate doctor exists
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(reviewDto.DoctorId);
            if (doctor == null)
                throw new Exception("Doctor not found");

            // Update review information
            result.DoctorId = reviewDto.DoctorId;
            result.DoctorNotes = reviewDto.DoctorNotes;
            result.IsReviewedByDoctor = true;
            result.ReviewedAt = DateTime.UtcNow;

            _unitOfWork.AIResults.Update(result);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AIResultDto>(result);
        }

        public async Task<bool> DeleteResultAsync(int resultId)
        {
            var result = await _unitOfWork.AIResults.GetByIdAsync(resultId);

            if (result == null)
                return false;

            // Delete image file if exists
            if (!string.IsNullOrEmpty(result.ImagePath))
            {
                var filePath = Path.Combine("wwwroot", result.ImagePath.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            _unitOfWork.AIResults.Delete(result);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
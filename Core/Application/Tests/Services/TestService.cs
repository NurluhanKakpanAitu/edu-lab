using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Tests.Dto;
using Application.Tests.Vm;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Tests.Services;

public class TestService(
    IApplicationDbContext dbContext,
    IHttpContextAccessor contextAccessor,
    HttpClient client
    ) : ITestService
{
    public async Task CreateTest(TestDto testDto, CancellationToken cancellationToken = default)
    {
        var module = await dbContext.Modules
            .Include(x =>x.Tests)
            .ThenInclude(x => x.Questions)
            .ThenInclude(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == testDto.ModuleId, cancellationToken);
        
        if (module == null)
            throw new Exception("Module not found");
        
        module.Tests.Add(new Test
        {
            Title = testDto.Title,
            Questions = testDto.Questions.Select(x => new Question
            {
                Title = x.Title,
                Answers = x.Answers.Select(y => new Answer
                {
                    Title = y.Title,
                    IsCorrect = y.IsCorrect
                }).ToList()
            }).ToList()
        });
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddQuestion(Guid testId, QuestionDto questionDto, CancellationToken cancellationToken = default)
    {
        var test = await dbContext.Tests
            .Include(x => x.Questions)
            .ThenInclude(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == testId, cancellationToken);
        
        if (test == null)
            throw new Exception("Test not found");
        
        test.Questions.Add(new Question
        {
            Title = questionDto.Title,
            Answers = questionDto.Answers.Select(x => new Answer
            {
                Title = x.Title,
                IsCorrect = x.IsCorrect
            }).ToList()
        });
    }

    public async Task DeleteTest(Guid testId, CancellationToken cancellationToken = default)
    {
        var test = await dbContext.Tests.FirstOrDefaultAsync(x => x.Id == testId, cancellationToken: cancellationToken);
        
        if (test == null)
            throw new Exception("Test not found");
        
        dbContext.Tests.Remove(test);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteQuestion(Guid questionId, CancellationToken cancellationToken = default)
    {
        var question = await dbContext.Questions.FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken: cancellationToken);
        
        if (question == null)
            throw new Exception("Question not found");
        
        dbContext.Questions.Remove(question);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAnswer(Guid answerId, CancellationToken cancellationToken = default)
    {
        var answer = await dbContext.Answers.FirstOrDefaultAsync(x => x.Id == answerId, cancellationToken: cancellationToken);
        
        if (answer == null)
            throw new Exception("Answer not found");
        
        dbContext.Answers.Remove(answer);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAnswer(Guid questionId, AnswerDto answerDto, CancellationToken cancellationToken = default)
    {
        var question = await dbContext.Questions
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken);
        
        if (question == null)
            throw new Exception("Question not found");
        
        question.Answers.Add(new Answer
        {
            Title = answerDto.Title,
            IsCorrect = answerDto.IsCorrect
        });
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TestVm> GetTest(Guid testId, CancellationToken cancellationToken = default)
    {
        var test = await dbContext.Tests
            .Include(x => x.Questions)
            .ThenInclude(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == testId, cancellationToken);

        if (test == null)
            throw new Exception("Test not found");

        return new TestVm
        {
            Id = test.Id,
            ModuleId = test.ModuleId,
            Title = test.Title,
            Questions = test.Questions.Select(question => new QuestionVm
            {
                Title = question.Title,
                Id = question.Id,
                ImagePath = question.ImagePath,
                Answers = question.Answers.Select(answer => new AnswerVm
                {
                    Title = answer.Title,
                    Id = answer.Id,
                    ImagePath = answer.ImagePath,
                    IsCorrect = answer.IsCorrect
                }).ToList()
            }).ToList()
        };
    }

    public async Task<QuestionVm> GetQuestion(Guid questionId, CancellationToken cancellationToken = default)
    {
        var question = await dbContext.Questions
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken);

        if (question == null)
            throw new Exception("Question not found");

        return new QuestionVm
        {
            Id = question.Id,
            Title = question.Title,
            ImagePath = question.ImagePath,
            Answers = question.Answers.Select(answer => new AnswerVm
            {
                Id = answer.Id,
                Title = answer.Title,
                ImagePath = answer.ImagePath,
                IsCorrect = answer.IsCorrect
            }).ToList()
        };
    }

    public async Task<AnswerVm> GetAnswer(Guid answerId, CancellationToken cancellationToken = default)
    {
        var answer = await dbContext.Answers.FirstOrDefaultAsync(x => x.Id == answerId, cancellationToken: cancellationToken);
        
        if (answer == null)
            throw new Exception("Answer not found");
        
        return new AnswerVm
        {
            Id = answer.Id,
            Title = answer.Title,
            ImagePath = answer.ImagePath,
            IsCorrect = answer.IsCorrect
        };
    }

    public async Task<IEnumerable<TestVm>> GetAllTests(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var tests = await dbContext.Tests
            .Include(x => x.Questions)
            .ThenInclude(x => x.Answers)
            .Where(x => x.ModuleId == moduleId)
            .ToListAsync(cancellationToken);
        
        return tests.Select(test => new TestVm
        {
            Id = test.Id,
            ModuleId = test.ModuleId,
            Title = test.Title,
            Questions = test.Questions.Select(question => new QuestionVm
            {
                Id = question.Id,
                Title = question.Title,
                ImagePath = question.ImagePath,
                Answers = question.Answers.Select(answer => new AnswerVm
                {
                    Id = answer.Id,
                    Title = answer.Title,
                    ImagePath = answer.ImagePath,
                    IsCorrect = answer.IsCorrect
                }).ToList()
            }).ToList()
        });
    }

    public async Task<IEnumerable<QuestionVm>> GetAllQuestions(Guid testId, CancellationToken cancellationToken = default)
    {
        var questions = await dbContext.Questions
            .Include(x => x.Answers)
            .Where(x => x.TestId == testId)
            .ToListAsync(cancellationToken);
        
        return questions.Select(question => new QuestionVm 
        {
            Id = question.Id,
            Title = question.Title,
            ImagePath = question.ImagePath,
            Answers = question.Answers.Select(answer => new AnswerVm
            {
                Id = answer.Id,
                Title = answer.Title,
                ImagePath = answer.ImagePath,
                IsCorrect = answer.IsCorrect
            }).ToList()
        });
    }

    public async Task<IEnumerable<AnswerVm>> GetAllAnswers(Guid questionId, CancellationToken cancellationToken = default)
    {
        var answers = await dbContext.Answers
            .Where(x => x.QuestionId == questionId)
            .ToListAsync(cancellationToken);
        
        return answers.Select(answer => new AnswerVm
        {
            Id = answer.Id,
            Title = answer.Title,
            ImagePath = answer.ImagePath,
            IsCorrect = answer.IsCorrect
        });
    }

    public async Task UpdateTest(Guid testId, TestDto testDto, CancellationToken cancellationToken = default)
    {
        var test = await dbContext.Tests
            .Include(x => x.Questions)
            .ThenInclude(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == testId, cancellationToken: cancellationToken);
        
        
        if (test == null)
            throw new Exception("Test not found");
        
        test.Title = testDto.Title;
        test.Questions = testDto.Questions.Select(x => new Question
        {
            Title = x.Title,
            ImagePath = x.ImagePath,
            Answers = x.Answers.Select(y => new Answer
            {
                Title = y.Title,
                IsCorrect = y.IsCorrect,
                ImagePath = y.ImagePath
            }).ToList()
        }).ToList();
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateQuestion(Guid questionId, QuestionDto questionDto, CancellationToken cancellationToken = default)
    {
        var question = await dbContext.Questions
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken: cancellationToken);
        
        if (question == null)
            throw new Exception("Question not found");
        
        question.Title = questionDto.Title;
        question.ImagePath = questionDto.ImagePath;
        question.Answers = questionDto.Answers.Select(x => new Answer
        {
            Title = x.Title,
            IsCorrect = x.IsCorrect,
            ImagePath = x.ImagePath
        }).ToList();
    }

    public async Task UpdateAnswer(Guid answerId, AnswerDto answerDto, CancellationToken cancellationToken = default)
    {
        var answer = await dbContext.Answers.FirstOrDefaultAsync(x => x.Id == answerId, cancellationToken: cancellationToken);
        
        if (answer == null)
            throw new Exception("Answer not found");
        
        answer.Title = answerDto.Title;
        answer.ImagePath = answerDto.ImagePath;
        answer.IsCorrect = answerDto.IsCorrect;
        
        await dbContext.SaveChangesAsync(cancellationToken); 
    }

    public async Task CreatePracticeWork(PracticeWorkDto practiceWorkDto, CancellationToken cancellationToken = default)
    {
        var module = await dbContext.Modules.FirstOrDefaultAsync(x => x.Id == practiceWorkDto.ModuleId, cancellationToken);
        
        if (module == null)
            throw new Exception("Module not found");
        
        var practiceWork = new PracticeWork
        {
            Title = practiceWorkDto.Title,
            Description = practiceWorkDto.Description,
            ImagePath = practiceWorkDto.ImagePath,
            ModuleId = practiceWorkDto.ModuleId
        };
        
        await dbContext.PracticeWorks.AddAsync(practiceWork, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePracticeWork(Guid practiceWorkId, PracticeWorkDto practiceWorkDto,
        CancellationToken cancellationToken = default)
    {
        var practiceWork = await dbContext.PracticeWorks.FirstOrDefaultAsync(x => x.Id == practiceWorkId, cancellationToken);
        
        if (practiceWork == null)
            throw new Exception("Practice work not found");
        
        
        practiceWork.Title = practiceWorkDto.Title;
        practiceWork.Description = practiceWorkDto.Description;
        practiceWork.ImagePath = practiceWorkDto.ImagePath;
        practiceWork.ModuleId = practiceWorkDto.ModuleId;
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePracticeWork(Guid practiceWorkId, CancellationToken cancellationToken = default)
    {
        var practiceWork = await dbContext.PracticeWorks.FirstOrDefaultAsync(x => x.Id == practiceWorkId, cancellationToken);
        
        if (practiceWork == null)
            throw new Exception("Practice work not found");
        
        dbContext.PracticeWorks.Remove(practiceWork);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PracticeWorkVm> GetPracticeWork(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var practiceWork = await dbContext.PracticeWorks
            .AsNoTracking()   
            .Include(x => x.Module)
            .FirstOrDefaultAsync(x => x.ModuleId == moduleId, cancellationToken);
        
        if (practiceWork == null)
            throw new Exception("Practice work not found");
        
        return new PracticeWorkVm
        {
            Id = practiceWork.Id,
            Title = practiceWork.Title,
            Description = practiceWork.Description,
            ImagePath = practiceWork.ImagePath,
        };
    }

    public async Task<GeminiResponse> GetPracticeWorkResult(PracticeWorkResultDto resultDto, CancellationToken cancellationToken = default)
    {
        const string apiKey = "AIzaSyBUs9vQy31-bpyvSoTQz6NxwJ1P23lwYR8";
        var practiceWork = await dbContext.PracticeWorks
            .Include(x => x.Module)
            .FirstOrDefaultAsync(x => x.Id == resultDto.PracticeWorkId, cancellationToken: cancellationToken);
        
        if (practiceWork == null)
            throw new Exception("Practice work not found");
        
        var practiceWorkDescription = practiceWork.Description["en"];
        
        const string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";
        
        var possibleAnswers = new[]
        {
            "The code is correct.",
            "The code is incorrect.",
            "The code is not working."
        };
        
        var promptToGemini = $$"""
                               Here is a description of the code: {{practiceWorkDescription}} and possible answers: {{string.Join(", ", possibleAnswers)}} and the code: {{resultDto.Code}} Give me like this: 
                               {
                                   "answer": "The code is correct.",
                                   "explanation": "The code is correct because...",
                                   "isCorrect": "true"
                               }
                               """;
        
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = promptToGemini }
                    }
                }
            }
        };
        
        
        var jsonRequest = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Send POST request
        var response = await client.PostAsync(url, content, cancellationToken);
        response.EnsureSuccessStatusCode();

        // Parse response
        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        dynamic? data = JsonConvert.DeserializeObject(jsonResponse);
        
        string? text = data?.candidates[0]?.content?.parts[0]?.text?.ToString();
        
        if (text == null)
            throw new Exception("Gemini response is empty");
        
        text = text.Replace("```", "");
        text = text.Replace("json", "");
        text = text.Trim();
        
        
        var geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(text);

        return geminiResponse ?? throw new Exception("Gemini response is empty");
    }

    public async Task<TestResultVm> GetTestResult(TestResultDto resultDto, CancellationToken cancellationToken = default)
    {
        var currentUserId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User not found");
        
        var test = await dbContext.Tests
            .Include(x => x.Questions)
            .ThenInclude(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == resultDto.TestId, cancellationToken);
        
        if (test == null)
            throw new Exception("Test not found");


        var user = await dbContext.Users
            .Include(x => x.UserTestResults)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(currentUserId), cancellationToken);
        
        if (user == null)
            throw new Exception("User not found");
        
        var userTestResult = new UserTestResult
        {
            TestId = resultDto.TestId,
            UserId = Guid.Parse(currentUserId),
            TestResults = resultDto.Results,
        };
        
        user.UserTestResults.Add(userTestResult);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return new TestResultVm
        {
            MaxScore = test.Questions.Count,
            Score = resultDto.Results.Count(x => x.IsCorrect),
        };
    }
}


public class GeminiResponse
{
    public string? Answer { get; set; }
    
    public string? Explanation { get; set; }
    
    public bool IsCorrect { get; set; }
}
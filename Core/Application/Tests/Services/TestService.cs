using Application.Interfaces;
using Application.Tests.Vm;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Services;

public class TestService(IApplicationDbContext dbContext) : ITestService
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
}
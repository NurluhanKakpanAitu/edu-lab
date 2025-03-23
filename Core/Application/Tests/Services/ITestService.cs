using Application.Tests.Dto;
using Application.Tests.Vm;

namespace Application.Tests.Services;

public interface ITestService
{
    Task CreateTest(TestDto testDto, CancellationToken cancellationToken = default);
    
    Task AddQuestion(Guid testId, QuestionDto questionDto, CancellationToken cancellationToken = default);
    
    
    Task DeleteTest(Guid testId, CancellationToken cancellationToken = default);
    
    Task DeleteQuestion(Guid questionId, CancellationToken cancellationToken = default);
    
    Task DeleteAnswer(Guid answerId, CancellationToken cancellationToken = default);
    
    Task AddAnswer(Guid questionId, AnswerDto answerDto, CancellationToken cancellationToken = default);
    
    
    Task<TestVm> GetTest(Guid testId, CancellationToken cancellationToken = default);
    
    Task<QuestionVm> GetQuestion(Guid questionId, CancellationToken cancellationToken = default);
    
    Task<AnswerVm> GetAnswer(Guid answerId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TestVm>> GetAllTests(Guid moduleId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<QuestionVm>> GetAllQuestions(Guid testId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<AnswerVm>> GetAllAnswers(Guid questionId, CancellationToken cancellationToken = default);
    
    Task UpdateTest(Guid testId, TestDto testDto, CancellationToken cancellationToken = default);
    
    Task UpdateQuestion(Guid questionId, QuestionDto questionDto, CancellationToken cancellationToken = default);
    
    Task UpdateAnswer(Guid answerId, AnswerDto answerDto, CancellationToken cancellationToken = default);
    
    
    Task CreatePracticeWork(PracticeWorkDto practiceWorkDto, CancellationToken cancellationToken = default);
    
    Task UpdatePracticeWork(Guid practiceWorkId, PracticeWorkDto practiceWorkDto, CancellationToken cancellationToken = default);
    
    Task DeletePracticeWork(Guid practiceWorkId, CancellationToken cancellationToken = default);
    
    Task<PracticeWorkVm> GetPracticeWork(Guid moduleId, CancellationToken cancellationToken = default);
    
    
    Task<GeminiResponse> GetPracticeWorkResult(PracticeWorkResultDto resultDto, CancellationToken cancellationToken = default);
}
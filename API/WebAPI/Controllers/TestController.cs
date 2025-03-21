using Application.Tests;
using Application.Tests.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(ITestService testService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] TestDto testDto, CancellationToken cancellationToken)
    {
        try
        {
            await testService.CreateTest(testDto, cancellationToken);
            return ResponseOk("Test created successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpPost("{testId}/questions")]
    public async Task<IActionResult> AddQuestion(Guid testId, [FromBody] QuestionDto questionDto,
        CancellationToken cancellationToken)
    {
        try
        {
            await testService.AddQuestion(testId, questionDto, cancellationToken);
            return ResponseOk("Question added successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpPost("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswer(Guid questionId, [FromBody] AnswerDto answerDto,
        CancellationToken cancellationToken)
    {
        try
        {
            await testService.AddAnswer(questionId, answerDto, cancellationToken);
            return ResponseOk("Answer added successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpDelete("{testId}")]
    public async Task<IActionResult> DeleteTest(Guid testId, CancellationToken cancellationToken)
    {
        try
        {
            await testService.DeleteTest(testId, cancellationToken);
            return ResponseOk("Test deleted successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpDelete("questions/{questionId}")]
    public async Task<IActionResult> DeleteQuestion(Guid questionId, CancellationToken cancellationToken)
    {
        try
        {
            await testService.DeleteQuestion(questionId, cancellationToken);
            return ResponseOk("Question deleted successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpDelete("answers/{answerId:guid}")]
    public async Task<IActionResult> DeleteAnswer(Guid answerId, CancellationToken cancellationToken)
    {
        try
        {
            await testService.DeleteAnswer(answerId, cancellationToken);
            return ResponseOk("Answer deleted successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }
    
    [HttpGet("{testId:guid}")]
    public async Task<IActionResult> GetTest(Guid testId, CancellationToken cancellationToken)
    {
        try
        {
            var test = await testService.GetTest(testId, cancellationToken);
            return ResponseOk(test);
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpGet("{testId:guid}/questions")]
    public async Task<IActionResult> GetAllQuestions(Guid testId, CancellationToken cancellationToken)
    {
        try
        {
            var questions = await testService.GetAllQuestions(testId, cancellationToken);
            return ResponseOk(questions);
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpGet("questions/{questionId:guid}")]
    public async Task<IActionResult> GetQuestion(Guid questionId, CancellationToken cancellationToken)
    {
        try
        {
            var question = await testService.GetQuestion(questionId, cancellationToken);
            return ResponseOk(question);
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpGet("questions/{questionId:guid}/answers")]
    public async Task<IActionResult> GetAllAnswers(Guid questionId, CancellationToken cancellationToken)
    {
        try
        {
            var answers = await testService.GetAllAnswers(questionId, cancellationToken);
            return ResponseOk(answers);
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpGet("answers/{answerId:guid}")]
    public async Task<IActionResult> GetAnswer(Guid answerId, CancellationToken cancellationToken)
    {
        try
        {
            var answer = await testService.GetAnswer(answerId, cancellationToken);
            return ResponseOk(answer);
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpPut("{testId}")]
    public async Task<IActionResult> UpdateTest(Guid testId, [FromBody] TestDto testDto, CancellationToken cancellationToken)
    {
        try
        {
            await testService.UpdateTest(testId, testDto, cancellationToken);
            return ResponseOk("Test updated successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpPut("questions/{questionId}")]
    public async Task<IActionResult> UpdateQuestion(Guid questionId, [FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
    {
        try
        {
            await testService.UpdateQuestion(questionId, questionDto, cancellationToken);
            return ResponseOk("Question updated successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpPut("answers/{answerId:guid}")]
    public async Task<IActionResult> UpdateAnswer(Guid answerId, [FromBody] AnswerDto answerDto, CancellationToken cancellationToken)
    {
        try
        {
            await testService.UpdateAnswer(answerId, answerDto, cancellationToken);
            return ResponseOk("Answer updated successfully.");
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

    [HttpGet("module/{moduleId:guid}/tests")]
    public async Task<IActionResult> GetAllTests(Guid moduleId, CancellationToken cancellationToken)
    {
        try
        {
            var tests = await testService.GetAllTests(moduleId, cancellationToken);
            return ResponseOk(tests);
        }
        catch (Exception ex)
        {
            return ResponseFail(ex.Message);
        }
    }

}
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using BLL.Interfaces;
using Common.Delta;
using Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.CrudTesting.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class CrudController<TEntity, TKey> : ControllerBase where TEntity : class, IEntity<TKey> where TKey : struct
{
    protected readonly IRepository<TEntity, TKey> _repository;

    public CrudController(IRepository<TEntity, TKey> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get([FromQuery] string include = "", [FromQuery] int? limit = 20, [FromQuery] int? offset = null)
    {
        var includes = ParseIncludeProperties(include);
        return Ok(await _repository.GetAsync(includeProperties: includes, limit: limit, offset: offset));
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> GetById(TKey id, [FromQuery] string include = "")
    {
        var includes = ParseIncludeProperties(include);
        var entity = await _repository.GetByIdAsync(id, includeProperties: includes);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity)
    {
        TEntity createdEntity = await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<TEntity>> Update(TKey id, [FromBody] TEntity entity)
    {
        if (!id.Equals(entity.Id))
        {
            return BadRequest("The id in the URL must match the id in the entity body.");
        }
        entity= await _repository.UpdateAsync(entity);
        return Ok(entity);
    }

    [HttpPatch("{id}")]
    public virtual async Task<ActionResult<TEntity>> Patch(TKey id, [FromBody] Delta<TEntity> delta)
    {
        TEntity entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        entity= await _repository.PatchAsync(id, delta);
        return Ok(entity);
    }

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult> Delete(TKey id)
    {
        TEntity entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    private Expression<Func<TEntity, object>>[] GetIncludeProperties(string include)
    {
        if (string.IsNullOrEmpty(include))
        {
            return new Expression<Func<TEntity, object>>[0];
        }
        string[] includeProperties = include.Split(",");
        PropertyInfo[] entityProperties = typeof(TEntity).GetProperties();
        List<Expression<Func<TEntity, object>>> expressions = new List<Expression<Func<TEntity, object>>>();
        foreach (string includeProperty in includeProperties)
        {
            PropertyInfo property = entityProperties.FirstOrDefault(p => p.Name.Equals(includeProperty, StringComparison.OrdinalIgnoreCase));
            if (property != null)
            {
                expressions.Add(e => property.GetValue(e));
            }
        }
        return expressions.ToArray();
    }

    public static IEnumerable<string> ParseIncludeProperties(string includeProperties)
    {
        if (string.IsNullOrEmpty(includeProperties))
        {
            yield break;
        }

        var propertyNames = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var propertyName in propertyNames)
        {
            var parts = propertyName.Trim().Split(new[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            foreach (var part in parts)
            {
                sb.Append(char.ToUpperInvariant(part[0]) + part.Substring(1));
            }

            yield return sb.ToString();
        }
    }

}
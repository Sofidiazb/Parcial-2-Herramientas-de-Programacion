using Parcial1.Data;
using Parcial1.Models;
using Microsoft.EntityFrameworkCore;

namespace Parcial1.Services;

public class EstudianteService : IEstudianteServices
{
    private readonly CursoContext _context;

    public EstudianteService(CursoContext context)
    {
        _context = context;
    }
    public void Create(Estudiante obj)
    {
        _context.Add(obj);
        _context.SaveChanges();
    }

    public void Delete(Estudiante obj)
    {
        _context.Remove(obj);
        _context.SaveChanges();
    }

    public List<Estudiante> GetAll()
    {
        return _context.Estudiante.Include(r => r.Cursos).ToList();
    }
    
    public List<Estudiante> GetAll(string filter)
    {
        var query = GetQuery();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.NombreAlumno.ToLower().Contains(filter.ToLower())
                || x.ApellidoAlumno.ToLower().Contains(filter.ToLower()) 
                || x.Dni.ToString().Contains(filter));
        }

        return query.ToList();
    }

    public Estudiante? GetById(int id)
    {
        var estudiante = _context.Estudiante
                .Include(r => r.Cursos)
                .FirstOrDefault(m => m.Id == id);

        return estudiante;
    }

    public void Update(Estudiante obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }

    private IQueryable<Estudiante> GetQuery()
    {
        return from estudiante in _context.Estudiante select estudiante;
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPelicula.Models;

namespace MVCPelicula.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly PeliculasDBContext _context;

        public PeliculasController(PeliculasDBContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(string textoABuscar)
        {
            if (_context.Peliculas == null)
            {
                return Problem("No se ha inicializado el contexo");
            }

            var peliculas = from p in _context.Peliculas.Include(p => p.Genero)
                            select p;

            if (!String.IsNullOrEmpty(textoABuscar))
            {
                peliculas = peliculas.Where(p => p.Titulo.Contains(textoABuscar));
            }

            //var peliculasDBContext = _context.Peliculas.Include(p => p.Genero);
            //return View(await peliculasDBContext.ToListAsync());
            return View(await peliculas.ToListAsync());
        }

        // POST: Peliculas
        [HttpPost]
        public string Index(string textoABuscar, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + textoABuscar;
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,FechaLanzamiento,GeneroId,Precio,Director")] Pelicula pelicula)
        {
            ModelState.Remove("Genero");
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,FechaLanzamiento,GeneroId,Precio,Director")] Pelicula pelicula)
        {
            ModelState.Remove("Genero");
            if (id != pelicula.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.ID == id);
        }
    }
}

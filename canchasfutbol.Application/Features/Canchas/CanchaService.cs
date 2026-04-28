using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static canchasfutbol.Application.Features.Canchas.CanchaService;

namespace canchasfutbol.Application.Features.Canchas
{
    public class CanchaService
    {
       
            private readonly ICanchasRepository _canchaRepository;

            public CanchaService(ICanchasRepository canchaRepository)
            {
                _canchaRepository = canchaRepository;
            }

            public async Task<IEnumerable<Cancha>> GetAllAsync()
            {
                return await _canchaRepository.GetAllAsync();
            }

            public async Task<Cancha?> GetByIdAsync(int id)
            {
                return await _canchaRepository.GetByIdAsync(id);
            }

            public async Task<Cancha> CreateAsync(Cancha cancha)
            {
                // Aquí podrías validar si ya existe una cancha con el mismo nombre
                var existe = await _canchaRepository.GetCanchaByName(cancha.Name);
                if (existe != null)
                {
                    throw new Exception("Ya existe una cancha con ese nombre.");
                }
                await _canchaRepository.AddAsync(cancha);
                return cancha;
            }

        public async Task<bool> UpdateAsync(Cancha cancha)
        {
            var existente = await _canchaRepository.GetByGuidAsync(cancha.Id);
                if (existente == null) return false;

                await _canchaRepository.UpdateAsync(cancha);
                return true;
            }

            public async Task<bool> DeleteAsync(Guid id)
            {
                var cancha = await _canchaRepository.GetByGuidAsync(id);
                if (cancha == null) return false;

                await _canchaRepository.DeleteAsync(cancha);
                return true;
            }
        

    }
}

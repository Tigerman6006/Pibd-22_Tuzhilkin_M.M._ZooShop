using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooShop.Data;
using ZooShop.Entities;
using ZooShop.Repositories.Interfaces;

namespace ZooShop.Repositories.Stubs
{
    public class AnimalRepositoryStub : RepositoryStub<Animal, int>, IAnimalRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<AnimalRepositoryStub> _logger;

        public AnimalRepositoryStub(
            DataContext context,
            ILogger<AnimalRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Animal? GetById(int id)
        {
            _logger.LogInformation(
                "Получение животного. SalableCode: {SalableCode}", id);

            try
            {
                var animal = _context.Animals
                    .FirstOrDefault(a => a.SalableCode == id);

                if (animal == null)
                {
                    _logger.LogWarning(
                        "Животное {SalableCode} не найдено", id);
                }
                else
                {
                    _logger.LogInformation(
                        "Животное {SalableCode} найдено", id);
                }

                return animal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения животного {SalableCode}", id);
                throw;
            }
        }

        public IEnumerable<Animal> GetAll()
        {
            _logger.LogInformation("Получение списка животных");

            try
            {
                var animals = _context.Animals.ToList();

                _logger.LogInformation(
                    "Получено животных: {Count}", animals.Count);

                return animals;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка животных");
                throw;
            }
        }

        public IEnumerable<Animal> GetAvailableAnimals()
        {
            _logger.LogInformation("Получение доступных животных");

            try
            {
                var animals = _context.Animals.ToList();

                _logger.LogInformation(
                    "Получено доступных животных: {Count}", animals.Count);

                return animals;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения доступных животных");
                throw;
            }
        }

        public void Add(Animal animal)
        {
            _logger.LogInformation(
                "Добавление животного: {AnimalName}", animal.Name);

            try
            {
                _context.Animals.Add(animal);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Животное добавлено. SalableCode: {SalableCode}",
                    animal.SalableCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка добавления животного {AnimalName}",
                    animal.Name);
                throw;
            }
        }

        public void Update(Animal animal)
        {
            _logger.LogInformation(
                "Обновление животного. SalableCode: {SalableCode}",
                animal.SalableCode);

            try
            {
                _context.Animals.Update(animal);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Животное {SalableCode} обновлено",
                    animal.SalableCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка обновления животного {SalableCode}",
                    animal.SalableCode);
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation(
                "Удаление животного. SalableCode: {SalableCode}", id);

            try
            {
                var animal = _context.Animals.Find(id);

                if (animal == null)
                {
                    _logger.LogWarning(
                        "Животное {SalableCode} не найдено для удаления", id);
                    return;
                }

                _context.Animals.Remove(animal);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Животное {SalableCode} удалено", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка удаления животного {SalableCode}", id);
                throw;
            }
        }
    }
}

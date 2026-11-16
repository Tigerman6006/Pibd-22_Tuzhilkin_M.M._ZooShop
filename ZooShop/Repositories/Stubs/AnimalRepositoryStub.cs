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
                "Получение животного. AnimalCode: {AnimalCode}", id);

            try
            {
                var animal = _context.Animals
                    .FirstOrDefault(a => a.AnimalCode == id);

                if (animal == null)
                {
                    _logger.LogWarning(
                        "Животное {AnimalCode} не найдено", id);
                }
                else
                {
                    _logger.LogInformation(
                        "Животное {AnimalCode} найдено", id);
                }

                return animal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения животного {AnimalCode}", id);
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
                    "Животное добавлено. AnimalCode: {AnimalCode}",
                    animal.AnimalCode);
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
                "Обновление животного. AnimalCode: {AnimalCode}",
                animal.AnimalCode);

            try
            {
                _context.Animals.Update(animal);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Животное {AnimalCode} обновлено",
                    animal.AnimalCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка обновления животного {AnimalCode}",
                    animal.AnimalCode);
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation(
                "Удаление животного. AnimalCode: {AnimalCode}", id);

            try
            {
                var animal = _context.Animals.Find(id);

                if (animal == null)
                {
                    _logger.LogWarning(
                        "Животное {AnimalCode} не найдено для удаления", id);
                    return;
                }

                _context.Animals.Remove(animal);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Животное {AnimalCode} удалено", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка удаления животного {AnimalCode}", id);
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelationshipApi.Data;
using RelationshipApi.Data.Entities;
using RelationshipApi.DTOs;

namespace RelationshipApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }

        [HttpGet("Character")]
        public async Task<ActionResult<List<Character>>> GetCharacter(int userId)
        {
            var characters = await _context.Characters
                .Where(c => c.UserId == userId)
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .ToListAsync();

            return characters;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Character>>> Get(int userId) // lấy tất cả userid có trong trong danh sách charactor
        //{
        //    var character = await _context.Characters
        //        .Where(c => c.UserId == userId)
        //        .Include(c => c.Weapon)
        //        .Include(c => c.Skills)
        //        .ToArrayAsync();
        //    return Ok(character);
        //}

        [HttpPost]
        public async Task<ActionResult<List<Character>>> Create(CreateCharacterDTO request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound();

            var newCharacter = new Character
            {
                Name = request.Name,
                RpgClass = request.RpgClass,
                User = user
            };

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await GetCharacter(newCharacter.UserId);
        }

        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> AddWeapon(AddWeaponDTO request)
        {
            var character = await _context.Characters.FindAsync(request.CharacterId);
            if (character == null)
                return NotFound();

            var newWeapon = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = character
            };

            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return character;
        }

        [HttpPost("skill")]
        public async Task<ActionResult<Character>> AddCharacterSkill(AddCharacterSkillDTO request)
        {
            var character = await _context.Characters
                .Where(c => c.Id == request.CharacterId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();
            if (character == null)
                return NotFound();

            var skill = await _context.Skills.FindAsync(request.SkillId);
            if (skill == null)
                return NotFound();

            character.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return character;
        }

        ////[HttpGet("{id}")]
        ////public async Task<ActionResult<List<Character>>> GetById(int id) // lấy theo id trong danh sách charactor
        ////{
        ////    var character = await _context.Characters
        ////        .Where(c => c.UserId == userId)
        ////        .ToArrayAsync();
        ////    return Ok(character);
        ////}
        //[HttpPost] //Tạo mới thông tin user
        //public async Task<ActionResult<List<Character>>> Create(CreateCharacterDTO request) // Tạo mới Charactor
        //{
        //    var user = await _context.Users.FindAsync(request.UserId); // Tìm kiếm User
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var newCharactor = new Character // Tạo mới tướng
        //    {
        //        Name = request.Name,
        //        RpgClass = request.RpgClass,
        //        User = user,
        //    };

        //    _context.Characters.Add(newCharactor);
        //    await _context.SaveChangesAsync();// Save ?
        //    return await Get(newCharactor.UserId);
        //}

        //[HttpPost("Weapon")]
        //public async Task<ActionResult<List<Character>>> AddWeapon(AddWeaponDTO request)
        //{
        //    var character = await _context.Characters.FindAsync(request.CharacterId);
        //    if (character == null)
        //    {
        //        return NotFound();
        //    }
        //    var newWeapon = new Weapon // tạo mới vũ khí
        //    {
        //        Name = request.Name,
        //        Damage = request.Damage,
        //        Character = character
        //    };
        //    _context.Weapons.Add(newWeapon);
        //    await _context.SaveChangesAsync();
        //    return Ok(character);
        //}
    }
}
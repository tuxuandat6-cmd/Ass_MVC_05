using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_btap04.Models; 

namespace mvc_btap04.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly QuanLySinhVienDbContext _context;

        public SinhVienController(QuanLySinhVienDbContext context)
        {
            _context = context;
        }

        // 1. READ: Danh sách sinh viên
        public async Task<IActionResult> Index()
        {
            var list = await _context.SinhViens.ToListAsync();
            return View(list);
        }

        // 2. CREATE: Form thêm mới
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sinhVien);
        }

        // 3. UPDATE: Form chỉnh sửa
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null) return NotFound();

            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SinhVien sinhVien)
        {
            if (id != sinhVien.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sinhVien);
        }

        // 4. DELETE: Xác nhận và xóa
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sinhVien = await _context.SinhViens.FirstOrDefaultAsync(m => m.Id == id);
            if (sinhVien == null) return NotFound();

            return View(sinhVien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
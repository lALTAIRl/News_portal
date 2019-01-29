//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using News_portal.DAL.Entities;
//using News_portal.DAL.Interfaces;
//using News_portal.ViewModels;

//namespace News_portal.Controllers
//{
//    public class NewsController : Controller
//    {
//        private readonly INewsRepository _newsRepository;
//        private readonly IMapper _mapper;
//        UserManager<ApplicationUser> _userManager;

//        public NewsController(INewsRepository newsRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
//        {
//            _newsRepository = newsRepository;
//            _mapper = mapper;
//            _userManager = userManager;
//        }

//        [Authorize(Roles = "admin")]
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [Authorize(Roles = "admin")]
//        [HttpPost]
//        public async Task<IActionResult> CreateNews(NewsCreateViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var news = _mapper.Map<News>(model);
//                news.DateOfCreating = DateTime.Now;
//                await _newsRepository.CreateNewsAsync(news);
//                return RedirectToAction("NewsCollection");
//            }
//            else
//            {
//                if (string.IsNullOrEmpty(model.Text))
//                {
//                    ModelState.AddModelError("", "Text required");
//                }
//                return View(model);
//            }
//        }

//        public async Task<IActionResult> ViewNews(int id)
//        {
//            var news = await _newsRepository.GetNewsByIdAsync(id);
//            var model = _mapper.Map<NewsViewModel>(news);
//            return View(model);
//        }

//        [Authorize(Roles = "admin")]
//        [HttpPost]
//        public async Task<IActionResult> PublishNews(int id)
//        {
//            var news = await _newsRepository.GetNewsByIdAsync(id);
//            news.IsPublished = true;
//            news.DateOfPublishing = DateTime.Now;
//            await _newsRepository.UpdateNewsAsync(news);
//            return RedirectToAction("NewsManagement");
//        }

//        [Authorize(Roles = "admin")]
//        [HttpPost]
//        public async Task<IActionResult> UnpublishNews(int id)
//        {
//            var news = await _newsRepository.GetNewsByIdAsync(id);
//            news.IsPublished = false;
//            await _newsRepository.UpdateNewsAsync(news);
//            return RedirectToAction("NewsManagement");
//        }

//        [Authorize(Roles = "admin")]
//        [HttpGet]
//        public async Task<IActionResult> EditNews(int id)
//        {
//            var news = await _newsRepository.GetNewsByIdAsync(id);
//            var model = _mapper.Map<NewsEditViewModel>(news);
//            return View(model);
//        }

//        [Authorize(Roles = "admin")]
//        [Authorize]
//        public async Task<IActionResult> UpdateNews(NewsEditViewModel newsEditViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var news = await _newsRepository.GetNewsByIdAsync(newsEditViewModel.Id);
//                _mapper.Map(newsEditViewModel, news);
//                await _newsRepository.UpdateNewsAsync(news);
//                return RedirectToAction("NewsManagement");
//            }
//            else
//            {
//                return RedirectToAction("EditNews", newsEditViewModel);
//            }
//        }

//        [Authorize(Roles = "admin")]
//        public async Task<IActionResult> DeleteNews(News news)
//        {
//            await _newsRepository.DeleteNewsAsync(news);
//            return RedirectToAction("NewsManagement");
//        }

//        public async Task<IActionResult> NewsCollection(int page = 1)
//        {
//            var news = await _newsRepository.GetAllNewsAsync();
//            news.OrderByDescending(s => s.DateOfCreating);
//            int pageSize = 5;
//            var count = news.Count();
//            var items = news.Skip((page - 1) * pageSize).Take(pageSize).ToList();
//            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
//            PageIndexViewModel viewModel = new PageIndexViewModel
//            {
//                PageViewModel = pageViewModel,
//                EnumNews = items
//            };
//            return View(viewModel);

//        }

//        [Authorize(Roles = "admin")]
//        public async Task<IActionResult> NewsManagement(int page = 1)
//        {
//            var news = await _newsRepository.GetAllNewsAsync();
//            news = news.OrderByDescending(s => s.DateOfCreating);
//            int pageSize = 10;
//            var count = news.Count();
//            var items = news.Skip((page - 1) * pageSize).Take(pageSize).ToList();
//            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
//            PageIndexViewModel viewModel = new PageIndexViewModel
//            {
//                PageViewModel = pageViewModel,
//                EnumNews = items
//            };
//            return View(viewModel);
//        }

//        [Authorize]
//        [HttpPost]
//        public async Task<IActionResult> AddToFavourites(int id)
//        {
//            var userId = _userManager.GetUserId(User);
//            await _newsRepository.AddNewsToUserFavourites(id, userId);
//            return RedirectToAction("NewsCollection");
//        }

//        [Authorize]
//        public async Task<IActionResult> ViewFavourites(int id)
//        {
//            var userId = _userManager.GetUserId(User);
//            var news = await _newsRepository.GetUsersFavouritesAsync(userId);
//            return View(news);
//        }

//        [Authorize]
//        [HttpPost]
//        public async Task<IActionResult> RemoveFromFavourites(int id)
//        {
//            var userId = _userManager.GetUserId(User);
//            await _newsRepository.RemoveNewsFromUserFavourites(id, userId);
//            return RedirectToAction("ViewFavourites");
//        }

//    }
//}
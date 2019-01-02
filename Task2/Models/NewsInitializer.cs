using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task2.Data;


namespace Task2.Models
{
    public class NewsInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if(!context.NewsCollection.Any())
            {
                context.NewsCollection.AddRange(
                    new News
                    {
                        Caption = "test news",
                        Text = "<b>first test news<b>",
                        ImageURL = "https://dpchas.com.ua/sites/default/files/u85/22_27.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption= "В Battlefield 5 добавят «умное сглаживание» на основе нейросетей",
                        Text= "<p>На закрытой презентации RTX 2060 компания Nvidia рассказала, что в Battlefiled 5 появится поддержка сглаживания на основе нейросетей.<br>",
                        ImageURL= "https://sun9-1.userapi.com/c849332/v849332034/f3072/y_yPmEKz2u8.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "HHuawei впервые за свою историю заработала за год более 100 миллиардов долларов",
                        Text = "<p>Китайская компания Huawei идет к своей цели семимильными шагами. А планы у нее довольно амбициозны: всего через несколько лет (а может, и через год) подвинуть конкурентов и стать абсолютным лидером на рынке смартфонов. И очередной рекорд на этом пути установлен: доход производителя за 2018 год превысил все ожидания.<br></ p >",
                        ImageURL = "https://sun9-14.userapi.com/c851036/v851036792/7cf6c/32Ri-6Y9Npw.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "Apple вдвое снизит производство новых iPhone XS Max",
                        Text = "<p>Аналитики не перестают твердить о том, что Apple здорово прокололась с новыми iPhone: особым успехом они не пользуются, в очереди за ними люди не выстраиваются. В результате компании приходится сокращать производство смартфонов.</p>",
                        ImageURL = "https://sun9-5.userapi.com/c850420/v850420792/7e6cc/vTtMtr2AqWA.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "В Android Messages появилась защита от спама",
                        Text = "<p>Компания Google обновила приложение Android Messages для обмена сообщениями, добавив сюда автоматическую защиту от спама.</p>",
                        ImageURL = "https://sun9-32.userapi.com/c850632/v850632999/7fea2/weYCOFT4yZs.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "Samsung готовит на 2019 год сразу несколько смарт-колонок",
                        Text = "<p>В последнее время появляется все больше новостей о флагманах Galaxy S10, но это далеко не единственные интересные новинки Samsung. Источники сообщают о том, что в следующем году компания выпустит сразу две модели смарт-колонок.</p>",
                        ImageURL = "https://sun9-22.userapi.com/c849120/v849120646/e7d1c/KbnuqGsBPYs.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "Huawei представила Y7 Pro 2019: бюджетник со Snapdragon 450 и емким аккумулятором за $170",
                        Text = "В последние дни 2018 года компания Huawei решила запрыгнуть в последний вагон и представить еще один смартфон — Huawei Y7 Pro 2019.",
                        ImageURL = "https://sun9-26.userapi.com/c844722/v844722243/165ecc/qnHGCYHtHjc.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "Похоже, что Sony готовит продолжение Uncharted и уже собирает новую команду",
                        Text = "<p>Видимо, в 2020 году Sony начнет разработку нового проекта, который относится к эксклюзивной франшизе компании.</p>",
                        ImageURL = "https://sun9-21.userapi.com/c846416/v846416243/15b107/StD0_1RNQ8o.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "Sony выпустила быструю зарядку мощностью 46,5 Вт",
                        Text = "<p>Компания Sony выпустила быстрое зарядное устройство с модельным номером CP-ADRM2, которое подойдет для смартфонов, планшетов, ноутбуков и камер.</p>",
                        ImageURL = "https://sun9-20.userapi.com/c850328/v850328215/a4f0a/unqff0svs0Y.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    },
                    new News
                    {
                        Caption = "Moto G7 на новых изображениях: каплевидный вырез, двойная камера и толстый «подбородок»",
                        Text = "<p>По слухам, в следующем году Lenovo и Motorola представят сразу четыре модели серии Moto G7. И вот в сети появились новые рендеры базовой версии смартфона в прозрачном стекле.</p>",
                        ImageURL = "https://sun9-19.userapi.com/c851416/v851416570/7fbf1/7NUoUuhDPTI.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    }
                    );
                context.SaveChanges();
            }
        }     
    }
}

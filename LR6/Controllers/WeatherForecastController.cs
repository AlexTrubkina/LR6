using LR6.Requests;
using LR6.Structures;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using LR6.Services;

namespace LR6.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : Controller
    {
        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return this.Json(new { result = "Hello World" });
        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("geron")]
        public async Task<IActionResult> Geron()
        {
            SecondRequest request = new SecondRequest(this.Request);

            // S = (p − a) * (p − b), Площадь треугольника по формул Герона
            // p = p = (a + b + c) : 2 - полупериметр

            double p = 0.5 * (request.A + request.B + request.С);

            double s = (p - request.A) * (p - request.B);

            Response res = new Response();
            res.Success = "yes";
            res.Result = s;
            res.Version = 1.1;
            return this.Json(res);
        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("create_calc")]
        public async Task<IActionResult> CreateCalc()
        {

            SecondRequest request = new SecondRequest(this.Request);
            string path = Path.Combine(Params.DataSrc, "json_settings.json");
            List<MyResults> content = new List<MyResults>();
            string str_content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(str_content))
            {
                content = JsonConvert.DeserializeObject<List<MyResults>>(str_content);

            }
            MyResults elem = new MyResults();
            elem.uuid = Guid.NewGuid().ToString();
            double p = 0.5 * (request.A + request.B + request.С);
            elem.result = (p - request.A) * (p - request.B);
            content.Add(elem);

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(content));

            return this.Json(elem);
        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("get_one_calc")]
        public async Task<IActionResult> GetOneCalc()
        {
            SecondRequest request = new SecondRequest(this.Request);

            string path = Path.Combine(Params.DataSrc, "json_settings.json");
            MyResults obj = null;
            string str_content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(str_content))
            {
                List<MyResults> content = JsonConvert.DeserializeObject<List<MyResults>>(str_content);

                foreach (var c in content)
                {
                    if (c.uuid == request.Uuid)
                    {
                        obj = c;
                    }
                }
            }
            return this.Json(obj);
        }


        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("update_calc")]

        public async Task<IActionResult> UpdateCalc()
        {
            SecondRequest request = new SecondRequest(this.Request);
            bool update = false;

            List<MyResults> content = new List<MyResults>();
            string path = Path.Combine(Params.DataSrc, "json_settings.json");
            string str_content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(str_content))
            {
                content = JsonConvert.DeserializeObject<List<MyResults>>(str_content);
                foreach(var b in content)
                {
                    if(b.uuid == request.Uuid)
                    {
                        double p = 0.5 * (request.A + request.B + request.С);
                        b.result = (p - request.A) * (p - request.B);
                        update = true;
                    }
                }
            }
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(content));

            return this.Json(new
            {
                result = update
            });

        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("del_calc")]

        public async Task<IActionResult> DelCalc()
        {
            SecondRequest request = new SecondRequest(this.Request);
            bool del = false;
            List<MyResults> content = new List<MyResults>();
            List<MyResults> final = new List<MyResults>();
            string path = Path.Combine(Params.DataSrc, "json_settings.json");
            string str_content = System.IO.File.ReadAllText(path);

            if (!string.IsNullOrEmpty(str_content))
            {
                content = JsonConvert.DeserializeObject<List<MyResults>>(str_content);
                foreach (var b in content)
                {
                    if (b.uuid != request.Uuid)
                    {
                        final.Add(b);
                        del = true;
                    }
                }
            }
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(final));

            return this.Json(new
            {
                result = del
            });
        }

    }
}
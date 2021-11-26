namespace LR6.Requests
{
    public class SecondRequest
    {
        public double A { get; set; } = 0.0;
        public double B { get; set; } = 0.0;
        public double С { get; set; } = 0.0;

        public string Uuid { get; set; } = "";

        public SecondRequest(HttpRequest request)
        {
            var form = request.Form.ToList();

            foreach (var param in form)
            {
                switch (param.Key.ToLower())
                {
                    case "a":
                        this.A = Convert.ToDouble(param.Value.ToArray()[0].ToString());
                        break;
                    case "b":
                        this.B = Convert.ToDouble(param.Value.ToArray()[0].ToString());
                        break;
                    case "c":
                        this.С = Convert.ToDouble(param.Value.ToArray()[0].ToString());
                        break;
                    case "uuid":
                        this.Uuid = param.Value.ToArray()[0].ToString();
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
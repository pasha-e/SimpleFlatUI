using SimpleFlatUI.Core;
using SimpleFlatUI.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFlatUI.MVVM.ViewModel
{
    internal class HomeViewModel : ObservableObject, IDisposable, ILogging
    {
        private string _usdRubMerchantRate = "-";

        public string UsdRubMerchantRate 
        {
            get { return _usdRubMerchantRate; }
            set
            {
                if (value.Equals(_usdRubMerchantRate))
                    return;

                _usdRubMerchantRate = value;

                OnPropertyChanged();
            }
             
        }

        private string _eurRubMerchantRate = "-";

        public string EurRubMerchantRate
        {
            get { return _eurRubMerchantRate; }
            set
            {
                if (value.Equals(_eurRubMerchantRate))
                    return;

                _eurRubMerchantRate = value;

                OnPropertyChanged();
            }

        }

        private Timer _timer;

        public event Action<string, LogMessage> LogMessageReceived;

        public HomeViewModel()
        {
            Init();
        }

        

        private void Init()
        {
            _timer = new Timer(OnTimerTick, null, 0, (int)TimeSpan.FromSeconds(60).TotalMilliseconds);            
        }

        private void OnTimerTick(object state)
        {
            GetExchangeRate();
        }

        private async void GetExchangeRate()
        {
            try
            {
                var url1 = "https://api.coingate.com/v2/rates/trader/buy/USD/RUB";
                var url2 = "https://api.coingate.com/v2/rates/trader/buy/EUR/RUB";

                var client = new HttpClient();

                var res = await client.GetAsync(url1);

                var content = await res.Content.ReadAsStringAsync();

                Object obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                UsdRubMerchantRate = content;

                LogMessageReceived?.Invoke(ToString(), new LogMessage(LogMessageType.Information, $"Recieving info: USD {UsdRubMerchantRate}"));

                res = await client.GetAsync(url2);

                content = await res.Content.ReadAsStringAsync();

                EurRubMerchantRate = content;

                LogMessageReceived?.Invoke(ToString(), new LogMessage(LogMessageType.Information, $"Recieving info: EUR {EurRubMerchantRate}"));
            }

            catch (Exception ex)
            {
                LogMessageReceived?.Invoke(ToString(), new LogMessage(ex));
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}

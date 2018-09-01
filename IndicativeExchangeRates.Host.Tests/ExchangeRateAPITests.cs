using System;
using System.Collections.Generic;
using IndicativeExchangeRates.FilterSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IndicativeExchangeRates.Host.Tests
{
    [TestClass]
    public class ExchangeRateAPITests
    {
        [TestMethod]        
        [HelperClass.ExpectedInnerException(typeof(System.Security.Authentication.AuthenticationException))]
        public void Constructor_CheckAuhentication_ReturnsAuthenticationException()
        {
            //I passed wrong UserName
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar1", "denemetry!4");
        }

        [TestMethod]
        public void Constructor_CheckAuhentication_ReturnsListOfAvailableOutputTypes()
        {
            //I passed wrong UserName
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            Assert.IsNotNull(era.GetAvailableOutputTypes, "Authentication did not succeeded");
            Assert.IsTrue(era.GetAvailableOutputTypes.Count > 0, "We couldn't get AvailableOutputTypes");
        }

        [TestMethod]
        public void Constructor_CreateInstance_ReturnsInstanceOfExchangeRateAPI()
        {
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            Assert.IsNotNull(era, "ExchangeRateAPI instance creation failed!");
        }

        [TestMethod]
        public void Constructor_GetAvailableOutputTypes_ReturnsGetAvailableOutputTypes()
        {
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            Assert.IsNotNull(era.GetAvailableOutputTypes, "AvailableOutputTypes is null!");
            Assert.IsTrue(era.GetAvailableOutputTypes.Count > 0, "AvailableOutputTypes is empty!");
        }

        [TestMethod]
        public void Constructor_GetResult_ReturnsAllCurrencyData()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            Assert.IsInstanceOfType(era, typeof(ExchangeRateAPI), "Created instance is not typeof ExchangeRateAPI");

            var response = Host.ExchangeRateAPI.GetResult();

            Assert.IsNotNull(response, "GetResult returned null");
        }

        [TestMethod]
        public void Constructor_GetResult_CheckReturnedDataIsNotEmpty()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            var response = Host.ExchangeRateAPI.GetResult();

            foreach (var item in response.Result)
            {
                Assert.IsNotNull(item, "Returned item can not be null.");
                Assert.IsInstanceOfType(item, typeof(Model.Currency), "Returned item should be typeof Model.Currency object");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Tarih), "Tarih can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Date), "Date can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Bulten_No), "Bulten_No can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.CrossOrder), "CrossOrder can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Kod), "Kod can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.CurrencyCode), "CurrencyCode can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Unit.ToString()), "Unit can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Isim), "Isim can not be empyt");
                Assert.IsTrue(!string.IsNullOrWhiteSpace(item.CurrencyName), "CurrencyName can not be empyt");
            }
        }

        [TestMethod]
        public void Constructor_GetPluginsFromPluginFolder_ReturnsAllPlugins()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            ICollection<PluginContracts.IPlugin> plugins = HelperClass.GenericPluginLoader<PluginContracts.IPlugin>.LoadPlugins(@"Plugins");

            Assert.IsNotNull(plugins, "We couldn't get the plugins");
            Assert.IsInstanceOfType(plugins, typeof(ICollection<PluginContracts.IPlugin>), "Returned plugins are not in expected type which is PluginContracts.IPlugin");
            Assert.IsTrue(plugins.Count > 0, "There is no pluging to use this API. Provide plugin.");
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetFilteredResult_FiltersAreNull_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            var result = era.GetFilteredData(null).Result;
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetFilteredResult_FiltersProvided_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            List<FilterSort.ExpressionFilter> filters = new List<FilterSort.ExpressionFilter>
            {
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="USD",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator=FilterSort.Enums.LogicalOperation.Or
                },
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="EUR",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator= FilterSort.Enums.LogicalOperation.Or
                }
            };

            var result = era.GetFilteredData(filters).Result;
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetFilteredResult_FiltersNotProvided_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");
            
            var result = era.GetFilteredData(null).Result;
        }

        [TestMethod]        
        public void API_GetFilteredResult_FiltersAndOutputTypeProvided_ReturnsListOfObject()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");
            era.RequestedOutput = "OutputXML";

            List<FilterSort.ExpressionFilter> filters = new List<FilterSort.ExpressionFilter>
            {
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="USD",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator=FilterSort.Enums.LogicalOperation.Or
                },
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="EUR",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator= FilterSort.Enums.LogicalOperation.Or
                }
            };

            var result = era.GetFilteredData(filters).Result;

            Assert.IsNotNull(result, "Every compulsory parameter provided but we could't get filtered data result.");
            Assert.IsTrue(!string.IsNullOrEmpty(result.ToString()), "Every compulsory parameter provided but we could't get filtered data result.");
        }
                
        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetSortedResult_SortParametersAreNull_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            var result = era.GetSortedData(null).Result;
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetSortedResult_SortParametersProvided_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            List<ExpressionSort> sorts = new List<ExpressionSort>
            {
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    SortDirection = FilterSort.Enums.SortDirection.Ascending
                },
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.ForexBuying,
                    SortDirection = FilterSort.Enums.SortDirection.Descending
                },
            };

            var result = era.GetSortedData(sorts).Result;
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetSortedResult_SortParametersNotProvided_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");
                        
            var result = era.GetSortedData(null).Result;
        }

        [TestMethod]
        public void API_GetFilteredResult_SortParametersAndOutputTypeProvided_ReturnsListOfObject()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");
            era.RequestedOutput = "OutputXML";

            List<ExpressionSort> sorts = new List<ExpressionSort>
            {
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    SortDirection = FilterSort.Enums.SortDirection.Ascending
                },
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.ForexBuying,
                    SortDirection = FilterSort.Enums.SortDirection.Descending
                },
            };

            var result = era.GetSortedData(sorts).Result;

            Assert.IsNotNull(result, "Every compulsory parameter provided but we could't get sorted data result.");
            Assert.IsTrue(!string.IsNullOrEmpty(result.ToString()), "Every compulsory parameter provided but we could't get sorted data result.");
        }
        
        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetFilteredAndSortedResult_FilterAndSortParametersAreNull_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            var result = era.GetFilteredAndSortedData(null, null).Result;
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetFilteredAndSortedResult_FiltersProvidedSortParametersNull_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            List<FilterSort.ExpressionFilter> filters = new List<FilterSort.ExpressionFilter>
            {
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="USD",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator=FilterSort.Enums.LogicalOperation.Or
                },
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="EUR",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator= FilterSort.Enums.LogicalOperation.Or
                }
            };            

            var result = era.GetFilteredAndSortedData(filters, null).Result;
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetSortedResult_FiltersNullSortParametersProvided_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

            List<ExpressionSort> sorts = new List<ExpressionSort>
            {
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    SortDirection = FilterSort.Enums.SortDirection.Ascending
                },
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.ForexBuying,
                    SortDirection = FilterSort.Enums.SortDirection.Descending
                },
            };

            var result = era.GetFilteredAndSortedData(null, sorts).Result;
        }

        [TestMethod]
        [HelperClass.ExpectedInnerException(typeof(NotImplementedException))]
        public void API_GetFilteredResult_FilterAndSortParametersProvidedButRequestedOutputTypeNotProvided_ReturnsNotImplementedException()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");
            
            List<FilterSort.ExpressionFilter> filters = new List<FilterSort.ExpressionFilter>
            {
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="USD",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator=FilterSort.Enums.LogicalOperation.Or
                },
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="EUR",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator= FilterSort.Enums.LogicalOperation.Or
                }
            };

            List<ExpressionSort> sorts = new List<ExpressionSort>
            {
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    SortDirection = FilterSort.Enums.SortDirection.Ascending
                },
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.ForexBuying,
                    SortDirection = FilterSort.Enums.SortDirection.Descending
                },
            };

            var result = era.GetFilteredAndSortedData(filters, sorts).Result;
        }

        [TestMethod]
        public void API_GetFilteredResult_FilterAndSortParametersAndOutputTypeProvided_ReturnsListOfObject()
        {
            //We create an instance of this class because we need to authenticate ourselves before requesting any data
            IndicativeExchangeRates.Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");
            era.RequestedOutput = "OutputXML";

            List<FilterSort.ExpressionFilter> filters = new List<FilterSort.ExpressionFilter>
            {
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="USD",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator=FilterSort.Enums.LogicalOperation.Or
                },
                new FilterSort.ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="EUR",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator= FilterSort.Enums.LogicalOperation.Or
                }
            };

            List<ExpressionSort> sorts = new List<ExpressionSort>
            {
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    SortDirection = FilterSort.Enums.SortDirection.Ascending
                },
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.ForexBuying,
                    SortDirection = FilterSort.Enums.SortDirection.Descending
                },
            };

            var result = era.GetFilteredAndSortedData(filters, sorts).Result;

            Assert.IsNotNull(result, "Every compulsory parameter provided but we could't get sorted data result.");
            Assert.IsTrue(!string.IsNullOrEmpty(result.ToString()), "Every compulsory parameter provided but we could't get sorted data result.");
        }

    }
}


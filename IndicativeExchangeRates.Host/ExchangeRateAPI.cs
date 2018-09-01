using IndicativeExchangeRates.FilterSort;
using IndicativeExchangeRates.Model;
using IndicativeExchangeRates.PluginContracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.Host
{
    public class ExchangeRateAPI : IDisposable
    {
        public List<string> GetAvailableOutputTypes => _plugins.Select(x => x.Key).ToList();

        private static string _username = string.Empty, _password = string.Empty;

        private string _requestedoutput = string.Empty;

        Dictionary<string, PluginContracts.IPlugin> _plugins;

        /// <summary>
        /// It must be specified string value from AvailableOutputTypes. If the specified value doesn't exist then you will get not implemented exception.
        /// </summary>
        public string RequestedOutput { set { this._requestedoutput = value; } }

        /// <summary>
        /// Produces Exchange Rate Result Among Available Output Types
        /// </summary>
        /// <param name="username">User name for authentication</param>
        /// <param name="password">Pasword for authentication</param>        
        public ExchangeRateAPI(string username, string password)
        {
            _username = username;
            _password = password;

            Log.Logger.Instance.Info($"UserName:{_username} created an instance of ExchangeRateAPI");

            Task.Run(() => CheckAuthentication()).Wait();

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} You must authenticate yourself"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            string CultureName = Thread.CurrentThread.CurrentCulture.Name;
            CultureInfo ci = new CultureInfo(CultureName);
            if (ci.NumberFormat.NumberDecimalSeparator != ".")
            {
                ci.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = ci;
            }

            _plugins = new Dictionary<string, PluginContracts.IPlugin>();

            ICollection<PluginContracts.IPlugin> plugins = GenericPluginLoader<PluginContracts.IPlugin>.LoadPlugins(@"Plugins");
            foreach (var item in plugins)
            {
                _plugins.Add(item.Name, item);
            }

            Log.Logger.Instance.Info($"UserName:{_username} - There are {_plugins.Count.ToString()} plugins found. {string.Join("|", _plugins.Select(p => p.Key))}");
        }

        /// <summary>
        /// Checks credentials against URL
        /// </summary>
        /// <returns></returns>
        private async Task CheckAuthentication()
        {
            #region Authentication

            Authentication.AuthService.UserName = _username;
            Authentication.AuthService.Password = _password;
            Authentication.AuthService service = Authentication.AuthService.Instance;
            System.Net.Http.HttpResponseMessage response = await Authentication.AuthService.hhtpResponse;
            
            string responseBody = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody.Trim('"').Equals(MD5Hash($"{_username}{_password}"), StringComparison.OrdinalIgnoreCase))
                {
                    Authentication.AuthService.IsAuthenticated = true;
                    Log.Logger.Instance.Info($"UserName:{_username} is authenticated");
                }
                else
                {
                    Authentication.AuthService.IsAuthenticated = false;
                    Log.Logger.Instance.Error(new System.Security.Authentication.InvalidCredentialException($"UserName:{_username} is authenticated but token is wrong"));
                    throw new System.Security.Authentication.InvalidCredentialException((response.StatusCode == System.Net.HttpStatusCode.OK ? "Token is not correct " : "") + response.ReasonPhrase);
                }
            }
            else
            {
                Authentication.AuthService.IsAuthenticated = false;
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} is rejected and got Unauthorized response"));
                throw new System.Security.Authentication.AuthenticationException((response.StatusCode == System.Net.HttpStatusCode.Unauthorized ? "You must provide credential. " : "") + response.Content.ToString() + " " + response.ReasonPhrase);
            }

            #endregion
        }

        private static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public async Task<object> GetAllData()
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetAllData() executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            object result = null;

            if (string.IsNullOrWhiteSpace(_requestedoutput))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (!GetAvailableOutputTypes.Contains(_requestedoutput, StringComparer.InvariantCultureIgnoreCase))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (_plugins.ContainsKey(_requestedoutput))
            {
                IPlugin plug = _plugins[_requestedoutput];
                result = await plug.GetAllData();
            }
            else
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            return result;
        }

        public async Task<object> GetFilteredData(List<ExpressionFilter> filters)
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetFilteredData(List<ExpressionFilter> filters) executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            object result = null;

            if (string.IsNullOrWhiteSpace(_requestedoutput))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (!GetAvailableOutputTypes.Contains(_requestedoutput, StringComparer.InvariantCultureIgnoreCase))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (_plugins.ContainsKey(_requestedoutput))
            {
                IPlugin plug = _plugins[_requestedoutput];
                result = await plug.GetFilteredData(filters);
            }
            else
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            return result;
        }

        public async Task<object> GetSortedData(List<ExpressionSort> sorts)
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetSortedData(List<ExpressionSort> sorts) executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            object result = null;

            if (string.IsNullOrWhiteSpace(_requestedoutput))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (!GetAvailableOutputTypes.Contains(_requestedoutput, StringComparer.InvariantCultureIgnoreCase))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (_plugins.ContainsKey(_requestedoutput))
            {
                IPlugin plug = _plugins[_requestedoutput];
                result = await plug.GetSortedData(sorts);
            }
            else
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            return result;
        }

        public async Task<object> GetFilteredAndSortedData(List<ExpressionFilter> filters, List<ExpressionSort> sorts)
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetFilteredAndSortedData(List<ExpressionFilter> filters, List<ExpressionSort> sorts) executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            object result = null;

            if (string.IsNullOrWhiteSpace(_requestedoutput))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type you want to get from here. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (!GetAvailableOutputTypes.Contains(_requestedoutput, StringComparer.InvariantCultureIgnoreCase))
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            if (_plugins.ContainsKey(_requestedoutput))
            {
                IPlugin plug = _plugins[_requestedoutput];
                result = await plug.GetFilteredAndSortedData(filters, sorts);
            }
            else
            {
                Log.Logger.Instance.Error(new NotImplementedException($"UserName:{_username} - You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes"));
                throw new NotImplementedException("You must provide the output type that already exists. To see list of available output types look at <classinstance>.GetAvailableOutputTypes");
            }

            return result;
        }

        /// <summary>
        /// Returns IQueryable<Model.Currency> object
        /// </summary>
        /// <returns></returns>
        public static async Task<IQueryable<Model.Currency>> GetResult()
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetResult() executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            var result = await Helper.GetExchangeRates.GetData();

            return result; ;
        }

        #region Filter

        /// <summary>
        /// Takes filters and returns IEnumerable<Model.Currency> object
        /// </summary>
        /// <param name="filters">List of IndicativeExchangeRates.FilterSort.ExpressionFilter object to filter data</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Model.Currency>> GetFilteredResult(List<ExpressionFilter> filters)
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetFilteredResult(List<ExpressionFilter> filters) executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            var expressionTree = CreateExpressionTree.ConstructFilterExpressionTree<Model.Currency>(filters);
            var anonymousFunc = expressionTree.Compile();
            var response = await GetResult();
            var result = response.Where(anonymousFunc);
            return result;
        }

        #endregion

        #region Sort

        /// <summary>
        /// Takes sort parameters and returns IEnumerable<Model.Currency> object
        /// </summary>
        /// <param name="sorts">List of IndicativeExchangeRates.FilterSort.ExpressionSort object to sort data</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Model.Currency>> GetSortedResult(List<ExpressionSort> sorts)
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetFilteredResult(List<ExpressionFilter> filters) executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            var response = await GetResult();

            var expressionTree = CreateExpressionTree.GenerateOrderMethodCall<Model.Currency>(response, sorts);

            var result = (IOrderedQueryable<Model.Currency>)response.Provider.CreateQuery<Model.Currency>(expressionTree);

            return result;
        }

        #endregion

        #region Filter And Sort

        /// <summary>
        /// Takes filters and sort parameters and returns IEnumerable<Model.Currency> object
        /// </summary>
        /// <param name="filters">List of IndicativeExchangeRates.FilterSort.ExpressionFilter object to filter data</param>
        /// <param name="sorts">List of IndicativeExchangeRates.FilterSort.ExpressionSort object to sort data</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Model.Currency>> GetFilteredAndSortedResult(List<ExpressionFilter> filters, List<ExpressionSort> sorts)
        {
            Log.Logger.Instance.Info($"UserName:{_username} - GetFilteredAndSortedResult(List<ExpressionFilter> filters, List<ExpressionSort> sorts) executed");

            if (Authentication.AuthService.IsAuthenticated == false)
            {
                Log.Logger.Instance.Error(new System.Security.Authentication.AuthenticationException($"UserName:{_username} - You must authenticate yourself!"));
                throw new System.Security.Authentication.AuthenticationException("You must authenticate yourself!");
            }

            var expressionTreeFilter = CreateExpressionTree.ConstructFilterExpressionTree<Model.Currency>(filters);
            var anonymousFunc = expressionTreeFilter.Compile();
            var response = await GetResult();
            var filteredresult = response.Where(anonymousFunc).AsQueryable<Model.Currency>();

            var expressionTreeSort = CreateExpressionTree.GenerateOrderMethodCall<Model.Currency>(filteredresult, sorts);
            var sortedresult = (IOrderedQueryable<Model.Currency>)filteredresult.Provider.CreateQuery<Model.Currency>(expressionTreeSort);

            return sortedresult;
        }

        #endregion

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // free managed resources
                    if (_plugins != null || _plugins.Count() > 0)
                    {
                        _plugins = null;
                    }

                    _username = string.Empty;
                    _password = string.Empty;
                    _requestedoutput = string.Empty;
                }

                disposed = true;
            }
        }

        #endregion IDisposable

        #region GenericPluginLoader

        #region Copyright 2013 Christoph Gattnar

        //Copyright 2013 Christoph Gattnar

        //Licensed under the Apache License, Version 2.0 (the "License");
        //you may not use this file except in compliance with the License.
        //You may obtain a copy of the License at

        //    http://www.apache.org/licenses/LICENSE-2.0

        //Unless required by applicable law or agreed to in writing, software
        //distributed under the License is distributed on an "AS IS" BASIS,
        //WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
        //See the License for the specific language governing permissions and
        //limitations under the License.

        #endregion Copyright 2013 Christoph Gattnar

        private static class GenericPluginLoader<T>
        {
            public static ICollection<T> LoadPlugins(string path)
            {
                string[] dllFileNames = null;

                if (Directory.Exists(path))
                {
                    dllFileNames = Directory.GetFiles(path, "*.dll");

                    ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                    foreach (string dllFile in dllFileNames)
                    {
                        AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                        Assembly assembly = Assembly.Load(an);
                        assemblies.Add(assembly);
                    }

                    Type pluginType = typeof(T);
                    ICollection<Type> pluginTypes = new List<Type>();
                    foreach (Assembly assembly in assemblies)
                    {
                        if (assembly != null)
                        {
                            Type[] types = assembly.GetTypes();

                            foreach (Type type in types)
                            {
                                if (type.IsInterface || type.IsAbstract)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (type.GetInterface(pluginType.FullName) != null)
                                    {
                                        pluginTypes.Add(type);
                                    }
                                }
                            }
                        }
                    }

                    ICollection<T> plugins = new List<T>(pluginTypes.Count);
                    foreach (Type type in pluginTypes)
                    {
                        T plugin = (T)Activator.CreateInstance(type);
                        plugins.Add(plugin);
                    }

                    return plugins;
                }

                return null;
            }
        }

        #endregion GenericPluginLoader
    }
}

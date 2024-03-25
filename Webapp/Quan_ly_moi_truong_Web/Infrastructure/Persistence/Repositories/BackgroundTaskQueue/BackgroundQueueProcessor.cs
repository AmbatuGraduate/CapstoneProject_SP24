﻿
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.BackgroundTaskQueue
{
    public class BackgroundQueueProcessor : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly HttpClient _httpClient;
        public BackgroundQueueProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5)); // Change the interval as needed
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(1));

                // Prepare the HTTP request
                var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7024/api/tree/AutoUpdate");


                // Send the request and get the response
                var response = await _httpClient.SendAsync(request);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Handle successful response
                    var content = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("API call successful: " + content);
                }
                else
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    // Handle unsuccessful response
                    System.Diagnostics.Debug.WriteLine("API call unsuccessful: " + reason);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                System.Diagnostics.Debug.WriteLine("Error calling API: " + ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

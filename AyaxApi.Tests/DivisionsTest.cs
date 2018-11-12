using AyaxApi.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AyaxApi.Tests
{
    public class DivisionsTest
    {
        private const string RequestUri = "/api/divisions";
        [Fact]
        public async Task TestGetAll()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync(RequestUri);
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task TestGet()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                        {
                            Id = 1,
                            Name = "Division1",
                            CreatedDateTime = DateTime.Now,
                        }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response = await client.GetAsync(RequestUri + "/1");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task TestCreate_Created()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                            {
                                Id = 1,
                                Name = "Division1",
                                CreatedDateTime = DateTime.Now,
                            }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
        [Fact]
        public async Task TestUpdate_NonFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                            {
                                Id = 2,
                                Name = "Division1",
                                CreatedDateTime = DateTime.Now,
                            }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response = await client.PutAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                            {
                                Id = 2,
                                Name = "Division1",
                                CreatedDateTime = DateTime.Now,
                            }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
        // All ok
        [Fact]
        public async Task TestUpdate_NoContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                            {
                                Id = 3,
                                Name = "Division1",
                                CreatedDateTime = DateTime.Now,
                            }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response = await client.PutAsync(RequestUri + "/3",
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                            {
                                Id = 3,
                                Name = "Division1",
                                CreatedDateTime = DateTime.Now,
                            }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
               response.EnsureSuccessStatusCode();
               Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
        [Fact]
        public async Task TestDelete_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                            {
                                Id = 4,
                                Name = "Division1",
                                CreatedDateTime = DateTime.Now,
                            }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response = await client.DeleteAsync(RequestUri + "/5");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
        [Fact]
        public async Task TestDelete_NoContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Division()
                            {
                                Id = 6,
                                Name = "Division1",
                                CreatedDateTime = DateTime.Now,
                            }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response = await client.DeleteAsync(RequestUri + "/6");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

    }
}



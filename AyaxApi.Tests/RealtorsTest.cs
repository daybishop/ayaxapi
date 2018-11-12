using AyaxApi.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AyaxApi.Tests
{
    public class RealtorsTest
    {
        private const string RequestUri = "/api/realtors";

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
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 1,
                            Firstname = "Firstname1",
                            Lastname = "Lastname1",
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
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 1,
                            Firstname = "Firstname1",
                            Lastname = "Lastname1",
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
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 2,
                            Firstname = "Firstname2",
                            Lastname = "Lastname2",
                            CreatedDateTime = DateTime.Now,
                        }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response = await client.PutAsync(RequestUri,
                    new StringContent(
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 2,
                            Firstname = "Firstname3",
                            Lastname = "Lastname3",
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
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 3,
                            Firstname = "Firstname2",
                            Lastname = "Lastname2",
                            CreatedDateTime = DateTime.Now,
                        }
                        ),
                        Encoding.UTF8,
                        "application/json"
                    )
                );
                response = await client.PutAsync(RequestUri + "/3",
                    new StringContent(
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 3,
                            Firstname = "Firstname3",
                            Lastname = "Lastname3",
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
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 4,
                            Firstname = "Firstname2",
                            Lastname = "Lastname2",
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
                        JsonConvert.SerializeObject(new Realtor()
                        {
                            Id = 6,
                            Firstname = "Firstname2",
                            Lastname = "Lastname2",
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



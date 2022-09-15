using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GrupoWebBackend.DomainPets.Resources;
using GrupoWebBackend.DomainPublications.Resources;
using GrupoWebBackend.DomainReport.Models;
using GrupoWebBackend.DomainSubscriptions.Resources;
using GrupoWebBackend.Security.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GrupoWebBackend.Tests
{
    [Binding]
    public class PublicationServiceTestSteps:WebApplicationFactory<Startup>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        private Uri _baseUri;

        private Task<HttpResponseMessage> Response
        {
            get;
            set;
        }
        public PublicationServiceTestSteps(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/publications is available")]
        public void GivenTheEndpointHttpsLocalhostApiVPublicationsIsAvailable(int port, int version)
        {
            _baseUri = new Uri($"https://localhost:{port}/api/v{version}/publications");
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions{BaseAddress = _baseUri});
        }

        [Given(@"A User is already stored")]
        public void GivenAUserIsAlreadyStored(Table table)
        {
            ScenarioContext.StepIsPending();
        }

        [When(@"A publication is sent")]
        public void WhenAPublicationIsSent(Table savePostResource)
        {
            var resource = savePostResource.CreateSet<SavePublicationResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = _client.PostAsync(_baseUri, content);
        }

        [Then(@"a response with status (.*) is received")]
        public async void ThenAResponseWithStatusIsReceived(int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualMessage = await Response.Result.Content.ReadAsStringAsync();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
            /*Assert.AreEqual(expectedStatusCode, actualMessage);*/

        }


        [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/publications/(.*) is available")]
        public void GivenTheEndpointHttpsLocalhostApiVPublicationsIsAvailable(int port, int version, int id)
        {
            _baseUri = new Uri($"https://localhost:{port}/api/v{version}/publications/{id}");
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions{BaseAddress = _baseUri});
        }

        [When(@"An update  publication is sent")]
        public void WhenAnUpdatePublicationIsSent(Table updatePostResource)
        {
            var resource = updatePostResource.CreateSet<SavePublicationResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, "application/json");
            Response = _client.PutAsync(_baseUri, content);
        }

        [When(@"An a delete request is sent")]
        public void WhenAnADeleteRequestIsSent()
        {
            Response = _client.DeleteAsync(_baseUri);
        }

        [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/publications/(.*) is not available")]
        public void GivenTheEndpointHttpsLocalhostApiVPublicationsIsNotAvailable(int port, int version, int id)
        {
            _baseUri = new Uri($"https://localhost:{port}/api/v{version}/publications/{id}");
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions{BaseAddress = _baseUri});
        }
        

        [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/Users/(.*)/publications is available now")]
        public void GivenTheEndpointHttpsLocalhostApiVUsersPublicationsIsAvailableNow(int port, int version, int id)
        {
            _baseUri = new Uri($"https://localhost:{port}/api/v{version}/Users/{id}/publications");
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions{BaseAddress = _baseUri});
        }

        [Then(@"a response with Message ""(.*)"" and status (.*) is received")]
        public async void ThenAResponseWithMessageAndStatusIsReceived(string expectedMessageCode, int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualMessage = await Response.Result.Content.ReadAsStringAsync();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
            Assert.AreEqual(expectedMessageCode, actualMessage);
        }

        [Given(@"A User is already stored for Publication")]
        public async void GivenAUserIsAlreadyStoredForPublication(Table saveUserResourceData)
        {
            var saveUserResource = saveUserResourceData.CreateSet<RegisterRequest>().First();
            var userUri = new Uri("https://localhost:5001/api/v1/users/auth/sign-up");
            var content = new StringContent(saveUserResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var userResponse = _client.PostAsync(userUri, content);
            var userResponseData = await userResponse.Result.Content.ReadAsStringAsync();
        }
        
        [Given(@"A Second User is already stored for Publication")]
        public async void GivenASecondUserIsAlreadyStoredForPublication(Table saveUserResourceData)
        {
            var saveUserResource = saveUserResourceData.CreateSet<RegisterRequest>().First();
            var userUri = new Uri("https://localhost:5001/api/v1/users/auth/sign-up");
            var content = new StringContent(saveUserResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var userResponse = _client.PostAsync(userUri, content);
            var userResponseData = await userResponse.Result.Content.ReadAsStringAsync();
        }

        [Given(@"a Pet is already stored for Publication")]
        public async void GivenAPetIsAlreadyStoredForPublication(Table savePetResourceData)
        {
            var savePetResource = savePetResourceData.CreateSet<SavePetResource>().First();
            var petUri = new Uri("https://localhost:5001/api/v1/Pets");
            var content = new StringContent(savePetResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var petResponse =  _client.PostAsync(petUri, content);
            var petResponseData = await petResponse.Result.Content.ReadAsStringAsync();
            var existingPet = JsonConvert.DeserializeObject<PetResource> (petResponseData);
            /*Pet = existingPet;*/
        }


        [Given(@"a Report is already stored for Publication")]
        public async void GivenAReportIsAlreadyStoredForPublication(Table saveReportResourceData)
        {
            var saveReportResource = saveReportResourceData.CreateSet<ReportResource>().First();
            var reportUri = new Uri("https://localhost:5001/api/v1/Report");
            var content = new StringContent(saveReportResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var reportResponse =  _client.PostAsync(reportUri, content);
            var reportResponseData = await reportResponse.Result.Content.ReadAsStringAsync();
        }

        [Given(@"a Subscription is already stored for Publication")]
        public async void GivenASubscriptionIsAlreadyStoredForPublication(Table saveSubscriptionResourceData)
        {
            var saveSubscriptionResource = saveSubscriptionResourceData.CreateSet<SaveSubscriptionResource>().First();
            var reportUri = new Uri("https://localhost:5001/api/v1/Subscriptions");
            var content = new StringContent(saveSubscriptionResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var reportResponse =  _client.PostAsync(reportUri, content);
            var reportResponseData = await reportResponse.Result.Content.ReadAsStringAsync();
        }
    }
}
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using GrupoWebBackend.DomainAdoptionsRequests.Resources;
using GrupoWebBackend.DomainPets.Resources;
using GrupoWebBackend.DomainPublications.Resources;
using GrupoWebBackend.DomainReport.Models;
using GrupoWebBackend.Security.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using GrupoWebBackend.Security.Resources;
using Newtonsoft.Json;

namespace GrupoWebBackend.Tests
{
    [Binding]
    public class AdoptionsRequestsServiceStep:WebApplicationFactory<Startup>
    {
        private readonly WebApplicationFactory<Startup>_factory;
        private HttpClient _client;
        private Uri _baseUri;
        private AdoptionsRequestsResource AdoptionsRequests { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }
        private RegisterRequest User { get; set; }
        private PetResource Pet { get; set; }
        private ReportResource Report { get; set; }
        private PublicationResource Publication { get; set; }

        public AdoptionsRequestsServiceStep(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Given(@"The Endpoint https://localhost:(.*)/api/v(.*)/AdoptionsRequests is available")]
        public void GivenTheEndpointHttpsLocalhostApiVAdoptionsRequestsIsAvailable(int port, int version)
        {
            _baseUri = new Uri($"https://localhost:{port}/api/v{version}"+"/AdoptionsRequests");
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions{BaseAddress = _baseUri});
        }

        [Given(@"A User is already stored for AdoptionsRequests")]
        public async void GivenAUserIsAlreadyStoredForAdoptionsRequests(Table saveUserResourceData)
        {
            var saveUserResource = saveUserResourceData.CreateSet<RegisterRequest>().First();
            var userUri = new Uri("https://localhost:5001/api/v1/users/auth/sign-up");
            var content = new StringContent(saveUserResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var userResponse = _client.PostAsync(userUri, content);
            var userResponseData = await userResponse.Result.Content.ReadAsStringAsync();
            /*var existingUser = JsonConvert.DeserializeObject<RegisterRequest> (userResponseData);*/
            /*Debug.WriteLine(saveUserResource.ToJson());
            Debug.WriteLine(content);*/
            /*User = existingUser;*/
        }
        
        [Given(@"A Second User is already stored for AdoptionsRequests")]
        public async void GivenASecondUserIsAlreadyStoredForAdoptionsRequests(Table saveUserResourceData)
        {
            var saveUserResource = saveUserResourceData.CreateSet<RegisterRequest>().First();
            var userUri = new Uri("https://localhost:5001/api/v1/users/auth/sign-up");
            var content = new StringContent(saveUserResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var userResponse =  _client.PostAsync(userUri, content);
            var userResponseData = await userResponse.Result.Content.ReadAsStringAsync();
            /*var existingUser = JsonConvert.DeserializeObject<RegisterRequest> (userResponseData);
            User = existingUser;*/
        }
        
        [Given(@"A Third User is already stored for AdoptionsRequests")]
        public async void GivenAThirdUserIsAlreadyStoredForAdoptionsRequests(Table saveUserResourceData)
        {
            var saveUserResource = saveUserResourceData.CreateSet<RegisterRequest>().First();
            var userUri = new Uri("https://localhost:5001/api/v1/users/auth/sign-up");
            var content = new StringContent(saveUserResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var userResponse =  _client.PostAsync(userUri, content);
            var userResponseData = await userResponse.Result.Content.ReadAsStringAsync();
        }

        [Given(@"A Pet already stored for AdoptionsRequests")]
        public async void GivenAPetAlreadyStoredForAdoptionsRequests(Table savePetResourceData)
        {
            var savePetResource = savePetResourceData.CreateSet<SavePetResource>().First();
            var petUri = new Uri("https://localhost:5001/api/v1/Pets");
            var content = new StringContent(savePetResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var petResponse =  _client.PostAsync(petUri, content);
            var petResponseData = await petResponse.Result.Content.ReadAsStringAsync();
            var existingPet = JsonConvert.DeserializeObject<PetResource> (petResponseData);
            Pet = existingPet;
        }

        [Given(@"A Publication already stored for AdoptionsRequests")]
        public async void GivenAPublicationAlreadyStoredForAdoptionsRequests(Table savePublicationResourceData)
        {
            var savePublicationResource = savePublicationResourceData.CreateSet<SavePublicationResource>().First();
            var publicationUri = new Uri("https://localhost:5001/api/v1/Publications");
            var content = new StringContent(savePublicationResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var publicationResponse =  _client.PostAsync(publicationUri, content);
            var publicationResponseData = await publicationResponse.Result.Content.ReadAsStringAsync();
            /*var existingPublication = JsonConvert.DeserializeObject<PublicationResource> (publicationResponseData);
            Publication = existingPublication;*/
        }

        [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/AdoptionsRequests/(.*) is available")]
        public void GivenTheEndpointHttpsLocalhostApiVAdoptionsRequestsIsAvailable(int port, int version, int id)
        {
            _baseUri = new Uri($"https://localhost:{port}/api/v{version}/AdoptionsRequests/{id}");
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions{BaseAddress = _baseUri});
        }



        [Given(@"A Report already stored for AdoptionsRequests")]
        public async void GivenAReportAlreadyStoredForAdoptionsRequests(Table saveReportResourceData)
        {
            var saveReportResource = saveReportResourceData.CreateSet<ReportResource>().First();
            var reportUri = new Uri("https://localhost:5001/api/v1/Report");
            var content = new StringContent(saveReportResource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var reportResponse =  _client.PostAsync(reportUri, content);
            var reportResponseData = await reportResponse.Result.Content.ReadAsStringAsync();
        }

        [When(@"A adoption request is sent from not Authenticated User")]
        public void WhenAAdoptionRequestIsSentFromNotAuthenticatedUser(Table saveAdoptionsRequests)
        {
            Debug.WriteLine("GATO");
            var resource = saveAdoptionsRequests.CreateSet<SaveAdoptionsRequestsResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = _client.PostAsync(_baseUri, content);
            Debug.WriteLine("GAAAATO");
        }

        [When(@"A adoption request is sent from Reported User")]
        public void WhenAAdoptionRequestIsSentFromReportedUser(Table saveAdoptionsRequests)
        {
            var resource = saveAdoptionsRequests.CreateSet<SaveAdoptionsRequestsResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = _client.PostAsync(_baseUri, content);
        }
        
        [When(@"A adoption request is sent")]
        public void WhenAAdoptionRequestIsSent(Table saveAdoptionsRequests)
        {
            var resource = saveAdoptionsRequests.CreateSet<SaveAdoptionsRequestsResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = _client.PostAsync(_baseUri, content);
        }
        
        [Then(@"A Response with Status (.*) is received")]
        public void ThenAResponseWithStatusIsReceived(int expectedStatus)
        {  
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [Then(@"AAdoptionRequests With Status (.*) is received")]
        public void ThenAAdoptionRequestsWithStatusIsReceived(int  expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [Then(@"A Response with Message ""(.*)"" and Status (.*) is received")]
        public async void ThenAResponseWithMessageAndStatusIsReceived(string expectedMessage, int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualMessage = await Response.Result.Content.ReadAsStringAsync();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        
        
        
    }
}
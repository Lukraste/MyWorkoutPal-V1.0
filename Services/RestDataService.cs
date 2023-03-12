using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyWorkoutPal.Models;

namespace MyWorkoutPal.Services
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        //Définition des URL endpoints de l'API qui est utilisée
        public RestDataService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _baseAddress = "http://localhost:5272";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        // Méthode asynchrone d'ajout d'un exercice en utilisant la requête POST de l'API
        public async Task AddExerciceAsync(Exercice exercice)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas d'accès internet...");
                return;
            }
            try
            {
                string jsonExercice = JsonSerializer.Serialize<Exercice>(exercice, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonExercice, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/exercice", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Exercice créé avec succès !!!");
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http...");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return;
        }

        // Méthode asynchrone permettant de récupérer la liste des tous les exercices via la requête GET de l'API

        public async Task<List<Exercice>> GetAllExercicesAsync()
        {
            List<Exercice> exercices = new List<Exercice>();

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas d'accès internet...");
                return exercices;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/exercices");

                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    exercices = JsonSerializer.Deserialize<List<Exercice>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http...");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return exercices;
        }

        // Méthode asynchrone de mise à jour d'un exercice en utilisant la requête UPDATE de l'API

        public async Task UpdateExerciceAsync(Exercice exercice)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas d'accès internet...");
                return;
            }
            try
            {
                string jsonExercice = JsonSerializer.Serialize<Exercice>(exercice, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonExercice, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/exercice/{exercice.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Exercice mit à jour avec succès !!!");
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http...");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return;
        }

        // Méthode asynchrone de suppression d'un exercice en utilisant la requête DELETE de l'API
    
        public async Task DeleteExerciceAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas d'accès internet...");
                return;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/exercice/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Exercice supprimé avec succès !!!");
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http...");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return;
        }
    }
}
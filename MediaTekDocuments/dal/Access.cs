using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using MediaTekDocuments.utils;
using Serilog;

namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = "http://localhost/rest_mediatekdocuments/";
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// méthode HTTP pour update
        private const string PUT = "PUT";
        /// <summary>
        /// méthode HTTP pour delete
        /// </summary>
        private const string DEL = "DELETE";
        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File("logs/errorlog.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            String authenticationString;
            try
            {
                string login = ConfigurationManager.AppSettings["ApiLogin"];
                string password = ConfigurationManager.AppSettings["ApiPassword"];
                authenticationString = login + ":" + password;
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Access.Access() - Erreur lors de la tentative de connexion à l'API : {0}", e.Message);
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if(instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre", null);
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon", null);
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public", null);
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre", null);
            return lesLivres;
        }

        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd", null);
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue", null);
            return lesRevues;
        }


        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            String jsonIdDocument = ConvertToJson("id", idDocument);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument, null);
            return lesExemplaires;
        }

        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire", "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.CreerExemplaire - Erreur lors de la création d'un exemplaire: {0}", ex.Message);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Retourne tous les suivis à partir de la BDD
        /// </summary>
        /// <returns></returns>
        public List<Suivi> GetAllSuivi()
        {
            List<Suivi> lesSuivis = TraitementRecup<Suivi>(GET, "suivi", null);
            return lesSuivis;
        }

        /// <summary>
        /// Retourne les commande d'un document type livre ou dvd
        /// </summary>
        /// <param name="idDocument">id du document</param>
        /// <returns></returns>
        public List<CommandeDocument> GetAllCommandeDocument(string idDocument)
        {
            String jsonIdDocument = ConvertToJson("id", idDocument);
            List<CommandeDocument> lesCommandesDocuments = TraitementRecup<CommandeDocument>(GET, "commandedocument/" + jsonIdDocument, null);
            return lesCommandesDocuments;
        }

        /// <summary>
        /// Récupère toutes les commandes et incrémente l'id maximum
        /// </summary>
        /// <returns>nouvel id formaté</returns>
        public string CreerNouvelIdCommande()
        {
            List<Commande> lesCommandes = TraitementRecup<Commande>(GET, "commandes", null);
            string nouvelId;
            if (lesCommandes != null && lesCommandes.Count > 0)
            {
                string idMax = lesCommandes.Max(c => c.Id);
                int nouvelIdNum = (int.Parse(idMax)) + 1;
                nouvelId = nouvelIdNum.ToString("00000");
            }
            else
            {
                nouvelId = "00001";
            }
            return nouvelId;
        }

        ///// <summary>
        ///// Créé une nouvelle commande de document
        ///// </summary>
        ///// <param name="commandeDocument"></param>
        ///// <returns>True si l'opération s'est bien passée</returns>
        public bool CreerNouvelleCommandeDocument(CommandeDocument commandeDocument)
        {
            String jsonCommandeDocument = JsonConvert.SerializeObject(commandeDocument, new CustomDateTimeConverter());
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(POST, "commandedocument", "champs=" + jsonCommandeDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.CreerNouvelleCommandeDocument - Erreur lors de la création d'une commande de document: {0}", ex.Message);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modification d'une commande en base de données
        /// </summary>
        /// <param name="commandeModifiee">commande à modifier</param>
        /// <returns>true si la modification a pu se faire (retour != null)</returns>
        public bool ModifierCommandeDocument(CommandeDocument commandeModifiee)
        {
            String jsonCommandeDocument = JsonConvert.SerializeObject(commandeModifiee, new CustomDateTimeConverter());
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(PUT, "commandedocument", "champs=" + jsonCommandeDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.ModifierCommande - Erreur lors de la modification d'une commande: {0}", ex.Message);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Suppression d'une commande en base de données
        /// </summary>
        /// <param name="idCommande">id de la Commande à supprimer</param>
        /// <returns>True si la suppression a réussi</returns>
        public bool SupprimerCommande(string idCommande)
        {
            String jsonIdCommande = ConvertToJson("id", idCommande);
            try
            {
                List<Commande> liste = TraitementRecup<Commande>(DEL, "commande/" + jsonIdCommande, null);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.SupprimerCommande - Erreur lors de la suppression d'une commande: {0}", ex.Message);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Création d'un abonnement en base de données
        /// </summary>
        /// <param name="abonnement"></param>
        /// <returns>True si l'opération est réussie</returns>
        public bool CreerNouvelAbonnement(Abonnement abonnement)
        {
            String jsonDocument = JsonConvert.SerializeObject(abonnement, new CustomDateTimeConverter());
            try
            {
                List<Abonnement> liste = TraitementRecup<Abonnement>(POST, "abonnement", "champs=" + jsonDocument);
                return (liste != null);
            }
            catch (Exception ex) 
            {
                Log.Error(ex, "Access.CreerNouvelAbonnement - Erreur lors de la création d'un abonnement: {0}", ex.Message);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Retourne les abonnements d'une revue
        /// </summary>
        /// <param name="idDocument">id du document</param>
        /// <returns></returns>
        public List<Abonnement> GetAllAbonnement(string idDocument)
        {
            String jsonIdDocument = ConvertToJson("id", idDocument);
            List<Abonnement> lesAbonnements = TraitementRecup<Abonnement>(GET, "abonnement/" + jsonIdDocument, null);
            return lesAbonnements;
        }

        /// <summary>
        /// Récupère les abonnements arrivant à expiration d'ici 30 jours
        /// </summary>
        /// <returns>liste des abonnements arrivant à expiration</returns>
        public List<AbonnementExpiration> GetAllAbonnementExpiration()
        {
            List<AbonnementExpiration> lesAbonnementsExpirant = TraitementRecup<AbonnementExpiration>(GET, "expiration", null);
            return lesAbonnementsExpirant;
        }

        /// <summary>
        /// Récupère les informations d'un utilisateur si le couple login/password est correct
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>un objet de type Utilisateur</returns>
        public Utilisateur CheckUtilisateur(string login, string password)
        {
            String jsonLogin = ConvertToJson("login", login);
            List<Utilisateur> utilisateur = TraitementRecup<Utilisateur>(GET, "utilisateur/" + jsonLogin, null);
            if (utilisateur != null && utilisateur.Count > 0)
            {
                Utilisateur utilisateurCheck = utilisateur[0];
                string utilisateurPwd = utilisateurCheck.Password;
                if (Utils.VerifyPassword(password, utilisateurPwd))
                {
                    return utilisateurCheck;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée dans l'url</param>
        /// <param name="parametres">paramètres à envoyer dans le body, au format "chp1=val1&chp2=val2&..."</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T> (String methode, String message, String parametres)
        {
            // trans
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message, parametres);
                // extraction du code retourné
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    // dans le cas du GET (select), récupération de la liste d'objets
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        // construction de la liste d'objets à partir du retour de l'api
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    Log.Error("Access.TraitementRecup - Le code de retour de l'API doit être 200: code={0}, message={1}", code, (String)retour["message"]);
                    Console.WriteLine("code erreur = " + code + " message = " + (String)retour["message"]);
                }
            }catch(Exception e)
            {
                Log.Fatal(e, "Access.TraitementRecup - Erreur lors de la tentative d'accès à l'API: {0}", e.Message);
                Console.WriteLine("Erreur lors de l'accès à l'API : "+e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Convertit en json un couple nom/valeur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="valeur"></param>
        /// <returns>couple au format json</returns>
        private String ConvertToJson(Object nom, Object valeur)
        {
            Dictionary<Object, Object> dictionary = new Dictionary<Object, Object>();
            dictionary.Add(nom, valeur);
            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

    }
}

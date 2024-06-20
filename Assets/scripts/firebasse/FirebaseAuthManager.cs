using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager Instance { get; private set; }
    public FirebaseAuth auth;
    public  FirebaseUser User { get; private set; }
    public string webClientId = "YOUR_WEB_CLIENT_ID.apps.googleusercontent.com";

    public InputField txtEmail;
    public InputField txtPass;
    public Text txtStatus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    public void SignInUser()
    {
        string email = txtEmail.text;
        string password = txtPass.text;
        StartCoroutine(AsyncSignInPass(email, password));
    }

    public void autenticate()
    {
        if (txtEmail.text == "")
        {
            txtStatus.text = "email is missing";
            return;
        }
        if (txtPass.text == "")
        {
            txtStatus.text = "pass is missing";
            return;
        }
        Debug.Log(txtEmail.text.ToString().Trim());
        Debug.Log(txtPass.text.ToString().Trim());
        StartCoroutine(AsyncSignInPass("leonelllopezvazquez4@gmail.com", "Peacesells2100@"));

    }

    public IEnumerator AsyncSignInPass(string email, string password)
    {
        var signInTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => signInTask.IsCompleted);

        if (signInTask.IsCanceled)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            txtStatus.text = "An error occurred";
            yield break;
        }
        if (signInTask.IsFaulted)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + signInTask.Exception);
            txtStatus.text = "An error occurred, not signed in";
            yield break;
        }

        Firebase.Auth.AuthResult result = signInTask.Result;
        txtStatus.text = "Login successful";
        User = result.User;
        Debug.LogFormat("User signed in successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);

        // Cambiar a la escena principal después de la autenticación exitosa
        SceneManager.LoadScene("MenuScene");
    }

    public void SignOut()
    {
        auth.SignOut();
        User = null;
        Debug.Log("User signed out.");
        SceneManager.LoadScene("LoginScene");  // Cambiar a la escena de inicio de sesión
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && User != null)
            {
                Debug.Log("Signed out " + User.UserId);
            }
            User = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + User.UserId);
            }
        }
    }

    private void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}
using Firebase;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class firebasemanagerAuth : MonoBehaviour
{
    private FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    string webclientId = "331338335516-e3hn6qlj9fcpu54m7d9mnv56ciosca59.apps.googleusercontent.com";

    public InputField txtEmail;
    public InputField txtPass;
    public Text txtstatus;
    
    private void Awake()
    {
        InitializeFirebase();

    }


    private void Start()
    {
        //register("leonelllopezvazquez4@gmail.com","Peacesells2100@");
        //signInpass("leonelllopezvazquez4@gmail.com", "Peacesells2100@");
        
        
        StartCoroutine(AsyncSignInPass("leonelllopezvazquez4@gmail.com", "Peacesells2100@"));
    }
    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //auth.StateChanged += AuthStateChanged;
        //AuthStateChanged(this, null);
    }

    void register(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    void signInpass(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                txtstatus.text = "an error was occured";
                return;
            }

            
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            user = result.User;
            txtstatus.text = "login succesfull";
        });
        
    }

    public IEnumerator AsyncSignInPass(string email, string password)
    {
        var signInTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => signInTask.IsCompleted);

        if (signInTask.IsCanceled)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            txtstatus.text = "an error was occured";
            yield break;
        }
        if (signInTask.IsFaulted)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + signInTask.Exception);
            txtstatus.text = "an error was occured, not signed in";
            yield break;
        }

        Firebase.Auth.AuthResult result = signInTask.Result;
        txtstatus.text = "login succesfull";
        user = result.User;
        Debug.LogFormat("User signed in successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);
    }

    void AuthorizeIn(string googleIdToken,string googleAccessToken)
    {
        Firebase.Auth.Credential credential =
        Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
           
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void autenticate()
    {
        if (txtEmail.text == "")
        {
            txtstatus.text = "email is missing";
            return;
        }
        if (txtPass.text == "")
        {
            txtstatus.text = "pass is missing";
            return;
        }
        Debug.Log(txtEmail.text.ToString().Trim());
        Debug.Log(txtPass.text.ToString().Trim());
        StartCoroutine(AsyncSignInPass("leonelllopezvazquez4@gmail.com", "Peacesells2100@"));
        
    }

    void getuser()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            string name = user.DisplayName;
            string email = user.Email;
            System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;
        }
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


}

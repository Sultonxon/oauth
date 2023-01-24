import { Component, Inject } from '@angular/core';
import { SocialAuthService, GoogleLoginProvider, SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'front';

  user: SocialUser | null;

  constructor(private authService: SocialAuthService,@Inject("SocialAuthServiceConfig") private signInOptions:any, private http: HttpClient) {
    this.user = null;
    console.log(this.signInOptions);

    this.authService.authState.subscribe((user: SocialUser) => {
      console.log(user);
      if(user){
        http.post<any>("https://localhost:5001/User/authenticate",{idToken:user.idToken})
          .subscribe((authToken:any) => {
            console.log(authToken.authToken);
            localStorage.setItem("authToken",authToken);
          });
      }
      this.user = user;
    });
  }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID, this.signInOptions).then((x:any) => { console.log(x);} );
  }

  signOut(): void {
    this.authService.signOut();
  }

}

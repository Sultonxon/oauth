import { Component, Inject } from '@angular/core';
import { SocialAuthService, GoogleLoginProvider, SocialUser } from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'front';

  user: SocialUser | null;

  constructor(private authService: SocialAuthService,@Inject("SocialAuthServiceConfig") private signInOptions:any) {
    this.user = null;
    console.log(this.signInOptions);

    this.authService.authState.subscribe((user: SocialUser) => {
      console.log(user);
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

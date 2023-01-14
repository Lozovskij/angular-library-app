import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule} from '@angular/material/table';
import { BooksCatalogComponent } from './books-catalog/books-catalog.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { ProfileComponent } from './profile/profile.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { HttpClientModule, HTTP_INTERCEPTORS }   from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    BooksCatalogComponent,
    TopBarComponent,
    BookDetailsComponent,
    ProfileComponent,
    LoginPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    HttpClientModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

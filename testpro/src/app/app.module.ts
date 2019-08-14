import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NewsComponent } from './project/news/news.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule, MatButtonModule, MatInputModule, MatTableModule, MatMenuModule, MatIconModule, MatToolbarModule } from '@angular/material';
import { ToastrModule } from 'ngx-toastr';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'

import { CustomFormsModule } from 'ng2-validation';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserComponent } from './project/user/user.component';
import { LoginComponent } from './project/user/login/login.component';
import { RegistrationComponent } from './project/user/registration/registration.component';

const appRoutes: Routes = [
  {path:'',redirectTo:'/user/login',pathMatch:'full'},
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  {path:'news',component:NewsComponent}
];


@NgModule({
  declarations: [
    AppComponent,
    NewsComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
    MatTableModule,
    MatMenuModule,
    MatIconModule,
    MatToolbarModule,
    MatCardModule,
    CustomFormsModule,
    RouterModule.forRoot(appRoutes),
    ReactiveFormsModule,
    CommonModule,
    ToastrModule.forRoot(),
    FlexLayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

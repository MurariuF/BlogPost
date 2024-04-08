import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Core/Components/navbar/navbar.component';
import { CategoryListComponent } from './Features/Category/category-list/category-list.component';
import { AddCategoryComponent } from './Features/Category/add-category/add-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { EditCategoryComponent } from './Features/Category/edit-category/edit-category.component';
import { BlogpostsListComponent } from './Features/blog-post/blogposts-list/blogposts-list.component';
import { AddBlogpostComponent } from './Features/blog-post/add-blogpost/add-blogpost.component';
import { MarkdownModule } from 'ngx-markdown';
import { EditBlogpostComponent } from './Features/blog-post/edit-blogpost/edit-blogpost.component';
import { ImageSelectorComponent } from './shared/components/image-selector/image-selector.component';
import { HomeComponent } from './Features/public/home/home.component';
import { BlogDetailsComponent } from './Features/public/blog-details/blog-details.component';
import { LoginComponent } from './Features/auth/login/login.component';
import { AuthInterceptor } from './Core/interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    BlogpostsListComponent,
    AddBlogpostComponent,
    EditBlogpostComponent,
    ImageSelectorComponent,
    HomeComponent,
    BlogDetailsComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MarkdownModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

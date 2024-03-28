import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Core/Components/navbar/navbar.component';
import { CategoryListComponent } from './Features/Category/category-list/category-list.component';
import { AddCategoryComponent } from './Features/Category/add-category/add-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EditCategoryComponent } from './Features/Category/edit-category/edit-category.component';
import { BlogpostsListComponent } from './Features/blog-post/blogposts-list/blogposts-list.component';
import { AddBlogpostComponent } from './Features/blog-post/add-blogpost/add-blogpost.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    BlogpostsListComponent,
    AddBlogpostComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

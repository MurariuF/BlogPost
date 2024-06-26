import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './Features/Category/category-list/category-list.component';
import { AddCategoryComponent } from './Features/Category/add-category/add-category.component';
import { EditCategoryComponent } from './Features/Category/edit-category/edit-category.component';
import { BlogpostsListComponent } from './Features/blog-post/blogposts-list/blogposts-list.component';
import { AddBlogpostComponent } from './Features/blog-post/add-blogpost/add-blogpost.component';
import { EditBlogpostComponent } from './Features/blog-post/edit-blogpost/edit-blogpost.component';
import { HomeComponent } from './Features/public/home/home.component';
import { BlogDetailsComponent } from './Features/public/blog-details/blog-details.component';
import { LoginComponent } from './Features/auth/login/login.component';
import { authGuard } from './Features/auth/guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'blog/:url', component: BlogDetailsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin/categories', component: CategoryListComponent, canActivate: [authGuard] },
  { path: 'admin/categories/add', component: AddCategoryComponent, canActivate: [authGuard] },
  { path: 'admin/categories/:id', component: EditCategoryComponent, canActivate: [authGuard] },
  { path: 'admin/blogposts', component: BlogpostsListComponent, canActivate: [authGuard] },
  { path: 'admin/blogposts/add', component: AddBlogpostComponent, canActivate: [authGuard] },
  { path: 'admin/blogposts/:id', component: EditBlogpostComponent, canActivate: [authGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

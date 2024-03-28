import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { BlogpostService } from '../services/blogpost.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.scss']
})
export class AddBlogpostComponent {

  model: AddBlogPost;

  constructor(private blogpostservice: BlogpostService, private router: Router) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      publishedDate: new Date(),
      author: '',
      isVisible: false
    };
  }

  onFormSubmit(): void {
    this.blogpostservice.createBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts');
      }
    })
  }

}

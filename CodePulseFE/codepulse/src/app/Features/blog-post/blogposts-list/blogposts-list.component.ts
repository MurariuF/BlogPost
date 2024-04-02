import { Component, OnInit } from '@angular/core';
import { BlogpostService } from '../services/blogpost.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';

@Component({
  selector: 'app-blogposts-list',
  templateUrl: './blogposts-list.component.html',
  styleUrls: ['./blogposts-list.component.scss']
})
export class BlogpostsListComponent implements OnInit {

  blogPosts$?: Observable<BlogPost[]>;

  constructor(private blogpostservice: BlogpostService) {}

  ngOnInit(): void {
    this.blogPosts$ = this.blogpostservice.getAllBlogPosts();
  }

}

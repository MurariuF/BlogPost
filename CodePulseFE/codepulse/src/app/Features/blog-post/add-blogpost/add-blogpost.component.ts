import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { BlogpostService } from '../services/blogpost.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../Category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../Category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.scss']
})
export class AddBlogpostComponent implements OnInit, OnDestroy {

  model: AddBlogPost;
  categories$?: Observable<Category[]>
  isImageSelectorVisible: boolean = false;

  imageSelectorSubscription?: Subscription;

  constructor(private blogpostservice: BlogpostService,
              private router: Router,
              private categoryService: CategoryService,
              private imageService: ImageService) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      publishedDate: new Date(),
      author: '',
      isVisible: false,
      categories: []
    };
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.imageSelectorSubscription = this.imageService.onSelectImage()
      .subscribe({
        next: (selectedImage) => {
          this.model.featuredImageUrl = selectedImage.url;
          this.closeImageSelector();
        }
      })
  }

  onFormSubmit(): void {
    console.log(this.model);
    this.blogpostservice.createBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts');
      }
    })
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }

}

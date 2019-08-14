import { Component, OnInit, EventEmitter } from '@angular/core';
import { News } from 'src/app/project/news';
import { NewsService } from 'src/app/project/news.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  news: News[] = [];
  new_news: News = new News(0, "", "", "");
  displayedColumns: string[] = ['ID', 'Title', 'Description', 'ImageName', 'Operations'];
  imageUrl: string = "/assets/img/default-image.png";
  fileToUpload: File = null;
  selectedFile;
  constructor(private newsServiceObj: NewsService, private router: Router) { }

  ngOnInit() {
    this.newsServiceObj.GetAll().subscribe(a => this.news = a);
  }

  GetAll() {

    this.newsServiceObj.GetAll().subscribe(a => this.news = a);
  }

  addNews() {
    this.new_news.ImageName = this.selectedFile;
  
    this.newsServiceObj.Create(this.new_news).subscribe(a => this.news.push(a));
    
  }

  upload(e) {
    console.log(e.target.files[0])
    var file = e.target.files[0]
    var reader = new FileReader();

    reader.addEventListener("load", () => {
      this.selectedFile = reader.result;
      // console.log(this.selectedFile)
    }, false);

    if (file) {
      reader.readAsDataURL(file);
    }

  }


  EditNews() {
    this.new_news.ImageName = this.selectedFile;
    
    this.newsServiceObj.Update(this.new_news.ID, this.new_news).subscribe(a => this.GetAll());
  }

  GetNewsData(id: number) {
    this.newsServiceObj.GetById(id).subscribe(a => this.new_news = a);
  }

  DeleteNews(id: number) {
    this.newsServiceObj.Delete(id).subscribe(a => {
      this.GetAll();
    })
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }


  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

}

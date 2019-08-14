import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { News } from 'src/app/project/news';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  baseURL : string = "http://localhost:61366/api/news/";
  constructor( private http:HttpClient ) { }

  GetAll(){
    return this.http.get<News[]>(this.baseURL);
  }


  GetById(id:number){
    return this.http.get<News>(this.baseURL + id);
  }

  Create(news : News){
    return this.http.post<News>(this.baseURL , news);
    
  }

  // postFile(fileToUpload: File) {
  //   const endpoint = this.baseURL+"Upload";
  //   const formData: FormData = new FormData();
  //   formData.append('Image', fileToUpload, fileToUpload.name);
  //   return this.http
  //     .post(endpoint, formData);
  // }


  Update(id:number, news:News){
    return this.http.put<News>(this.baseURL+ id, news);
  }

  Delete(id:number){
    return this.http.delete<News>(this.baseURL+id);
  }
}

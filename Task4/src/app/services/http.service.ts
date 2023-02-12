import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FileDetails } from '../models/fileDetails';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  backendUrl = 'https://localhost:7245/Files';  // URL to web api

  constructor(private http: HttpClient) { }

  uploadFile(formData: FormData): Observable<any> {
    
    return this.http.post(this.backendUrl + '/uploadFile', formData);
  }

  getFileInfo(fileName: string): Observable<FileDetails> {

    return this.http.get<FileDetails>(this.backendUrl + '/FileDetails/?filename=' + fileName);
  }

  getAllFilesInfo(): Observable<FileDetails[]> {

    return this.http.get<FileDetails[]>(this.backendUrl + '/FilesDetails');
  }

}

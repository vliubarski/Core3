import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FileDetails } from '../models/fileDetails';
import { HttpService } from './http.service';
import { map, Observable, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilesService {

  constructor(private httpService: HttpService) { }

  uploadFile(file: File) :Observable<any>{
      const formData = new FormData();
      formData.append('formfile', file);

      return this.httpService.uploadFile(formData);
  }

  getFileInfo(fileName: string):Observable<FileDetails>{

    return this.httpService.getFileInfo(fileName);    
  }

  getAllFileInfo():Observable<FileDetails[]>{

    return this.httpService.getAllFilesInfo();    
  }

}

import { Component, OnInit } from '@angular/core';
import { FileDetails } from 'src/app/models/fileDetails';
import { FilesService } from 'src/app/services/files.service';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-table',
  templateUrl: './file.component.html',
  styleUrls: ['./file.component.css'],
  providers: [DatePipe]
})
export class FileComponent implements OnInit {

  public filesDetails: FileDetails[] = [];
  public fileDetails: FileDetails = new  FileDetails();

  constructor(private fileService: FilesService, private datepipe: DatePipe) { }

  ngOnInit(): void {
  }

  public file!: File;
  public fileName = '';
  public fileInfoName = '';

  public onFileUpload(){
    this.fileService.uploadFile(this.file)
    .subscribe();
;
    this.fileName = '';
  }

  onFileSelected(event: any) {
    this.file = event.target.files[0];
    this.fileName = this.file.name;
  }

  onFileInfo(fileInfoName : string) {
    this.fileService.getFileInfo(fileInfoName)
    .subscribe(filesDetails=>
        this.fileDetails = filesDetails
    );
  }
    
  onAllFileInfo() {
    this.fileService.getAllFileInfo()
    .subscribe(filesDetails=>
        this.filesDetails = filesDetails
    );
  }
    
  formatDate(date: Date): string | null {
    return this.datepipe.transform(new Date(date), 'yyyy-MM-dd');
  }
      
}

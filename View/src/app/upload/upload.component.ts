import { Component } from '@angular/core';  
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent {
private isUploadBtn: boolean = true;  
constructor(private http: HttpClient) {  
}  
//file upload event  
fileChange(event) {  
  
let fileList: FileList = event.target.files;  
if (fileList.length > 0) {  
let file: File = fileList[0];  
let formData: FormData = new FormData();  
formData.append('uploadFile', file, file.name);  

//headers.append('Content-Type', 'json');  
//headers.append('Accept', 'application/json');  

let apiUrl1 = "http://localhost:58653/api/Upload/UploadJsonFile";  
this.http.post(apiUrl1, formData)  
.pipe(map((response: any) => response.json()))
.subscribe(res => {
});
}
}
}

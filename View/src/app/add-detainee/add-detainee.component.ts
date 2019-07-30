import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Detainee } from '../models/detainee';
import { FormGroup, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-add-detainee',
  templateUrl: './add-detainee.component.html',
  styleUrls: ['./add-detainee.component.css']
})
export class AddDetaineeComponent implements OnInit {
  bookFormGroup: FormGroup;
  imageUrl: string = "/assets/img/find.png";
  filetoUpload: File = null;
  @Output() detainee: Detainee  = new Detainee();
  @Output() searchItem: EventEmitter<any> = new EventEmitter();
  path: string;
  Logo: string;
  fileToUpload: File = null;
  constructor(private fb: FormBuilder, private http: HttpClient) 
  {
    this.bookFormGroup = this.fb.group({})
  }

  ngOnInit() {}
  checkIt() {}

  handleFileInput(file: FileList)
  {
  this.fileToUpload = file.item(0);
  var reader = new FileReader();
  reader.onload = (event:any) =>
  {
    this.imageUrl = event.target.result;
  }
  reader.readAsDataURL(this.fileToUpload);
  }

  uploadFile()
  {
    ///TODO Upload file service
    let formData: FormData = new FormData(); 
    formData.append('uploadFile',   this.fileToUpload, this.fileToUpload.name);  
    this.detainee.photo = this.fileToUpload.name;
    let apiUrl1 = "http://localhost:58653/api/Upload/UploadJsonFile";  
    this.http.post(apiUrl1, formData)  
    .subscribe(hero => {
    });
  }

    handleUpload(e):void{
      this.Logo = e.target.value;
  }

  anyFunction(){}

  sendDetaineeToDetention() {
    this.detainee.photo = this.fileToUpload.name;
    var copy = Object.assign({}, this.detainee);

    this.searchItem.emit(copy);
  }
}

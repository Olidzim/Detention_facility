import { Component, OnInit, Output, Input, EventEmitter, Inject } from '@angular/core';
import { Release} from '../models/release';
import { ReleaseService } from '../services/release.service';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-add-release',
  templateUrl: './add-release.component.html',
  styleUrls: ['./add-release.component.css']
})
export class AddReleaseComponent implements OnInit {

  @Input() detaineeID: number;
  @Input() detentionID: number;
  @Output() release: Release = new Release;
  @Output() toNewRelease = new EventEmitter<Release>();

  constructor(private releaseService: ReleaseService, private sharedService: SharedService) { }

  getEmployeeFromDetail(employeeIDForChange: number){  
    console.log("releaseadd")
    this.release.releasedByEmployeeID = employeeIDForChange;
    }

    cancel()
    {
      console.log("Check")
      this.sharedService.changeReleaseCancel(false)
    }

  save()
  {
    this.release.detaineeID = this.detaineeID;
    this.release.detentionID = this.detentionID;
  this.releaseService.createRelease(this.release)
  .subscribe((data: Release) => 
  {
    let sendDelivery = new Release; 
    console.log(data);
    this.toNewRelease.emit(this.release);
  });
  //console.log(this.delivery);
  
}

  ngOnInit() {
  }

}
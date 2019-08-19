import { Input, Component, OnInit , Inject} from '@angular/core';
import {ReleaseService} from '../services/release.service';
import {SmartDelivery} from '../models/smartdelivery';
import {SmartEmployee} from '../models/smartemployee';
import { Release } from '../models/release';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EmployeeSearchComponent } from '../employee-search/employee.search.component';
import { SharedService } from '../services/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-release',
  templateUrl: './release-detail.component.html',
  styleUrls: ['./release-detail.component.css']
})
export class ReleaseDetailComponent implements OnInit {
  add: boolean = false;
  isRelease = false;
  res: string;
  change: boolean = false;
  @Input() detaineeID: number;
  @Input() detentionID: number;
  release: Release;
  defaultEmployeeID: number;

  constructor(
    private releaseService: ReleaseService, 
    private sharedService: SharedService, 
    private router: Router) { }

  ngOnInit() {
    this.getRelease(this.detaineeID, this.detentionID)
    this.sharedService.ifChange = false;
    this.res = this.router.url.substring(0, 15);
    this.sharedService.cancelReleaseStatus.subscribe(status => {   
        console.log(status)
        this.add = status   
        console.log(this.add)      
    })  
  }


  saveChanges()  
  { 
    console.log(this.release)
    this.releaseService.updateRelease(this.release)
    .subscribe(data => this.release = data);
    this.change = false;
    this.sharedService.ifChange = false;
    this.sharedService.default = true;
  }

  ngOnChanges() {
    this.getRelease(this.detaineeID, this.detentionID)
   }

   cancel() {
    this.release.releasedByEmployeeID = this.defaultEmployeeID;
    this.change = false;
    this.sharedService.ifChange = false;
    this.sharedService.default = true;
  }


  changeForm(){
    this.change = true;
    this.sharedService.ifChange = true;
    
  }

  getEmployeeFromDetail(employeeIDForChange: number){  
    this.release.releasedByEmployeeID = employeeIDForChange;
    }

  getRelease(detaineeID, detentionID): void {
    if (detaineeID == undefined)
    {
      detaineeID = 0;
    }
    if (detentionID == undefined)
    {
      detentionID = 0;
    }
    this.releaseService.getReleaseByIDs(detaineeID, detentionID)
    .subscribe(
    res => 
    {if (res==undefined)
    {      
     this.release = {}      
    }
    else
    {
      this.isRelease = true;
      this.release = res;  
      this.defaultEmployeeID = this.release.releasedByEmployeeID;      
    }
    });
  }

  getReleaseFromAdd(release: Release){   
    this.release = release;
    this.add = false;
    console.log(this.release)
    this.getRelease(this.detaineeID, this.detentionID)
   // this.ngOnInit();
  }

  addForm()
  {
    this.sharedService.changeReleaseCancel(true)
    this.add = true;
  }

    deleteForm()
  {
   
    this.releaseService.deleteRelease(this.release.releaseID)
    .subscribe(release => 
      {console.log(release)
        this.sharedService.changeDeliveryCancel(false)
        this.ngOnInit();
      });
      this.isRelease = false
   
  }
}

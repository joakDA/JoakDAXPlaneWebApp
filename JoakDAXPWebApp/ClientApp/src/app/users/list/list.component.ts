import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../_services';
import { first } from 'rxjs/operators';
import {User} from '../../_models';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  users: User[] = null;
  usersCount: number;
  dtOptions: any = {};
  constructor(private accountService: AccountService, private route: ActivatedRoute,
              private router: Router) {}

  onAddUserBtnClicked() {
    this.router.navigate(['/account/add']);
  }

  ngOnInit() {
    $('body').addClass('df');
    this.dtOptions = {
      dom: 'Blfrtip',
      autoWidth: false,
      buttons: [
        {
          text: 'Add Pilot',
          className: 'custom-btn fas fa-print',
          key: '1',
          action: () => {
            this.onAddUserBtnClicked();
          }
        }
        ],
      pagingType: 'full_numbers',
      pageLength: 10,
      serverSide: true,
      processing: false,
      info: true,
      ajax: (dataTablesParameters: any, callback) => {
        this.accountService.getAll(dataTablesParameters)
          .pipe(first())
          .subscribe(resp => {
              this.users = resp.data;
              this.usersCount = resp.data.length;
              $('.dataTables_length>label>select, .dataTables_filter>label>input').addClass('form-control-sm');
              callback({
                recordsTotal: resp.recordsTotal,
                recordsFiltered: resp.recordsFiltered,
                data: []
              });
              if (this.usersCount > 0) {
                $('.dataTables_empty').css('display', 'none');
              }
            }
            );
      },
      scrollCollapse: true,
      scrollY: '70vh',
      // tslint:disable-next-line:max-line-length
      columns: [{data: 'id'}, { data: 'firstName'}, {data: 'lastName'}, {data: 'email'}, {data: 'username'}, {data: 'actions', searchable: false, orderable: false}],
    };
  }

  deleteUser(id: string) {
    const user = this.users.find(x => x.id === id);
    user.isDeleting = true;
    this.accountService.delete(id)
      .pipe(first())
      .subscribe(() => this.users = this.users.filter(x => x.id !== id));
  }
}

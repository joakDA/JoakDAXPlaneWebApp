import {Component, OnInit} from '@angular/core';
import {Flight} from '../_models/flight';
import {FlightService} from '../_services/flight.service';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.css']
})
export class FlightComponent implements OnInit {
  flights: Flight[] = null;
  flightsCount: number;
  dtOptions: any = {};

  constructor(private flightService: FlightService) {
  }

  ngOnInit(): void {
    $('body').addClass('df');
    this.dtOptions = {
      dom: 'Blfrtip',
      autoWidth: false,
      buttons: [],
      pagingType: 'full_numbers',
      pageLength: 25,
      serverSide: true,
      processing: false,
      info: true,
      ajax: (dataTablesParameters: any, callback) => {
        this.flightService.getAll(dataTablesParameters)
          .pipe(first())
          .subscribe(resp => {
              this.flights = resp.data;
              this.flightsCount = resp.data.length;
              $('.dataTables_length>label>select, .dataTables_filter>label>input').addClass('form-control-sm');
              callback({
                recordsTotal: resp.recordsTotal,
                recordsFiltered: resp.recordsFiltered,
                data: []
              });
              if (this.flightsCount > 0) {
                $('.dataTables_empty').css('display', 'none');
              }
            }
          );
      },
      scrollCollapse: true,
      scrollY: '70vh',
      // tslint:disable-next-line:max-line-length
      columns: [
        {data: 'id'}, {data: 'eventDateTime'}, {data: 'flightEventType'}, {data: 'location'}, {data: 'latitude'}, {data: 'longitude'},
        {data: 'distanceFromIdeal'}, {data: 'glideslopeScore'}, {data: 'verticalSpeed'}, {data: 'maxForce'}, {data: 'pitch'}
      ],
    };
  }

}

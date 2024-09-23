import { Component, OnDestroy, OnInit } from "@angular/core";
import { MediasService } from "../medias.service";
import { Observable, Subject } from "rxjs";
import { Item } from "@root/components/models";

@Component({
	selector: "medias-list",
	templateUrl: "./medias-list.component.html",
})
export class MediasListComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	public listTitle$: Observable<string>;
	public listData$: Observable<Item[]>;
	constructor(private service: MediasService) {}

	public ngOnInit(): void {
		this.listData$ = this.service.data$;
		this.listTitle$ = this.service.title$;
	}

	public ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
	}
}

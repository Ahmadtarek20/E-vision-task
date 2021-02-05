import { Component, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { ProductService } from 'src/app/core/services/product.service';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  Product: Product[] = [];
  closeResult: string = '';
  searchText: any;


  constructor(private productserv: ProductService,
    private modalService: NgbModal,
    private alertifyService: AlertifyService,

   ) { }

  ngOnInit() {
    this.loadProdects();
  }
  open(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }
  deleteProduct(id: any,status: any) {
    const params = {
     id: id
    }
    this.productserv.deleteProduct(params).subscribe(res => {
      this.alertifyService.success("Done Delete Product")
      this.loadProdects();
    },error =>{
      this.alertifyService.error("Error")
    });
  }
  downloadFile(){
     window.open('https://localhost:44380/api/product/ExportProducts',"_blank")
  }
  loadProdects(){
    this.productserv.getProducts().subscribe((Product : Product[])=>{
       this.Product = Product;
    },error =>{
      this.alertifyService.error("Error")
    });
  }
}

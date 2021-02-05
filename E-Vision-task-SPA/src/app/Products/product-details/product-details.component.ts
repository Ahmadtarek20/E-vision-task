import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/core/services/product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  product: any;
  productId = +this.activatedRoute.snapshot.params['id'];
  constructor(private activatedRoute: ActivatedRoute,private productService: ProductService) { }

  ngOnInit(): void {
    this.getCategory();
  }

  getCategory(){
    this.productService.getProduct(this.productId).subscribe((res )=>{
       this.product = res;
    });
  }
}

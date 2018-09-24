
# Install from NuGet
-Microsoft.EntityFrameworkCore
-Microsoft.EntityFrameworkCore.SqlServer

## Model Binding
If you want to read the data from JSON/Text you need to add [from body] without it will not read the product.
Since the the method name is post it can be called from URL: /api/product with type POST (Help of POST man for test)
```sh
    [HttpPost("")]
    public IActionResult Post(Product product)
    {
        return Ok();
    }
```
should be
```sh
	[HttpPost("")]
    public IActionResult Post([FromBody]Product product)
    {
        return Ok();
    }
```


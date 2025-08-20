# API Versioning Demo - .NET Core

This project demonstrates various API versioning strategies in .NET Core.

## Running the Demo

```bash
dotnet run
```

Navigate to `https://localhost:7000/swagger` to see the Swagger UI with versioned APIs.

## Versioning Strategies Demonstrated

### 1. URL Path Versioning (Recommended)
- **V1**: `GET /api/v1.0/products`
- **V2**: `GET /api/v2.0/products`

### 2. Header Versioning
- Add header: `X-Version: 1.0` or `X-Version: 2.0`
- **Example**: `GET /api/users` with `X-Version: 2.0`

### 3. Query Parameter Versioning
- **Example**: `GET /api/orders?version=2.0`

## API Examples

### Products API V1 (Deprecated)
```bash
# Get all products - Notice deprecation headers in response
curl -X GET "https://localhost:7000/api/v1.0/products" -v
# Response includes:
# Deprecation: true
# Deprecation-Date: Sat, 01 Jun 2024 00:00:00 GMT
# Sunset: Tue, 31 Dec 2024 23:59:59 GMT

# Get specific product
curl -X GET "https://localhost:7000/api/v1.0/products/1"

# Create product
curl -X POST "https://localhost:7000/api/v1.0/products" \
  -H "Content-Type: application/json" \
  -d '{"name":"Tablet","price":299.99,"category":"Electronics"}'
```

### Legacy API (Removed)
```bash
# This will return HTTP 410 Gone
curl -X GET "https://localhost:7000/api/legacy/status"

# This still works but shows deprecation warning
curl -X GET "https://localhost:7000/api/legacy/items"
```

### Products API V2 (Enhanced)
```bash
# Get all products (with new fields)
curl -X GET "https://localhost:7000/api/v2.0/products"

# Get products including inactive
curl -X GET "https://localhost:7000/api/v2.0/products?includeInactive=true"

# Update product status (new endpoint)
curl -X PATCH "https://localhost:7000/api/v2.0/products/1/status" \
  -H "Content-Type: application/json" \
  -d 'false'
```

### Header Versioning Example
```bash
# V1 Users
curl -X GET "https://localhost:7000/api/users" \
  -H "X-Version: 1.0"

# V2 Users (enhanced response)
curl -X GET "https://localhost:7000/api/users" \
  -H "X-Version: 2.0"
```

## Key Features Demonstrated

1. **Multiple versioning strategies**
2. **Backward compatibility**
3. **Swagger documentation for each version**
4. **Gradual API evolution**
5. **Version deprecation handling**
6. **Deprecation headers (RFC 8594)**
7. **Usage monitoring and logging**
8. **HTTP 410 Gone for removed APIs**

## Best Practices Shown

- ✅ Semantic versioning (1.0, 2.0)
- ✅ Default version handling
- ✅ Multiple version readers
- ✅ Clear API documentation
- ✅ Consistent response formats
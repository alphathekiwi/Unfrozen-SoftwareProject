from django.contrib import admin

# Register your models here.
from .models import Author
from .models import Article
from .models import Tag

admin.site.register(Author)
admin.site.register(Article)
admin.site.register(Tag)

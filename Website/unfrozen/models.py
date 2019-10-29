from django.db import models

class Author(models.Model):
    author_full_name = models.CharField(max_length=200)
    author_info = models.TextField()
    author_photo = models.ImageField(upload_to = 'images/')
    author_email = models.CharField(max_length=200)
    author_link = models.CharField(max_length=200)
    def __str__(self):
        return self.author_full_name

class Tag(models.Model):
    tag_id = models.AutoField(primary_key=True)
    tag_name = models.CharField(max_length=100)
    def __str__(self):
        return self.tag_name

    def get_all_tags(self):
        tags = self.tag_name.all()
        return tags

class Article(models.Model):
    article_id = models.AutoField(primary_key=True)
    article_author = models.ForeignKey(Author, on_delete=models.CASCADE)
    article_name = models.CharField(max_length=200)
    article_content = models.TextField()
    article_date_added = models.DateTimeField('date published')
    article_source = models.CharField(max_length=400)
    articles_tags = models.ManyToManyField(Tag)
    def __str__(self):
        return self.article_name

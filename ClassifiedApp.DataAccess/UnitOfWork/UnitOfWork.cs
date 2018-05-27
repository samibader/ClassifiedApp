using ClassifiedApp.DataAccess.Repositories;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable,IUnitOfWork
    {
        #region Private member variables...

        private ClassifiedAppDbContext _context = null;
        private EntityBaseRepository<User> _userRepository;
        private EntityBaseRepository<Category> _categoryRepository;
        private EntityBaseRepository<SubCategory> _subCategoryRepository;
        private EntityBaseRepository<Token> _tokenRepository;
        private EntityBaseRepository<ActivationCode> _activationCodeRepository;
        private EntityBaseRepository<PropertyDefinition> _propertyDefinitionRepository;
        private EntityBaseRepository<PropertyValue> _propertyValueRepository;
        private EntityBaseRepository<SubPropertyDefinition> _subPropertyDefinitionRepository;
        private EntityBaseRepository<FavoriteUser> _favoriteUserRepository;
        private EntityBaseRepository<FavoriteClassified> _favoriteClassifiedRepository;
        private EntityBaseRepository<Classified> _classifiedRepository;
        private EntityBaseRepository<ClassifiedImage> _classifiedImageRepository;
        private EntityBaseRepository<Admin> _adminRepository;
        private EntityBaseRepository<ReportUserTicket> _reportUserTicketRepository;
        private EntityBaseRepository<ReportClassifiedTicket> _reportClassifiedTicketRepository;
        private EntityBaseRepository<Notification> _notificationRepository;
        private EntityBaseRepository<Like> _likeRepository;
        private EntityBaseRepository<Rate> _rateRepository;
        private EntityBaseRepository<FeedbackTicket> _feedbackTicketRepository;
        private EntityBaseRepository<Device> _deviceRepository;
        #endregion

        public UnitOfWork()
        {
            _context = new ClassifiedAppDbContext(); 
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        public EntityBaseRepository<Category> CategoryRepository
        {
            get
            {
                if (this._categoryRepository == null)
                    this._categoryRepository = new EntityBaseRepository<Category>(_context);
                return _categoryRepository;
            }
        }

        public EntityBaseRepository<SubCategory> SubCategoryRepository
        {
            get
            {
                if (this._subCategoryRepository == null)
                    this._subCategoryRepository = new EntityBaseRepository<SubCategory>(_context);
                return _subCategoryRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public EntityBaseRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new EntityBaseRepository<User>(_context);
                return _userRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public EntityBaseRepository<Token> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                    this._tokenRepository = new EntityBaseRepository<Token>(_context);
                return _tokenRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for Activation Code repository.
        /// </summary>
        public EntityBaseRepository<ActivationCode> ActivationCodeRepository
        {
            get
            {
                if (this._activationCodeRepository == null)
                    this._activationCodeRepository = new EntityBaseRepository<ActivationCode>(_context);
                return _activationCodeRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for Activation Code repository.
        /// </summary>
        public EntityBaseRepository<PropertyDefinition> PropertyDefinitionRepository
        {
            get
            {
                if (this._propertyDefinitionRepository == null)
                    this._propertyDefinitionRepository = new EntityBaseRepository<PropertyDefinition>(_context);
                return _propertyDefinitionRepository;
            }
        }
        public EntityBaseRepository<PropertyValue> PropertyValueRepository
        {
            get
            {
                if (this._propertyValueRepository == null)
                    this._propertyValueRepository = new EntityBaseRepository<PropertyValue>(_context);
                return _propertyValueRepository;
            }
        }
        public EntityBaseRepository<SubPropertyDefinition> SubPropertyDefinitionRepository
        {
            get
            {
                if (this._subPropertyDefinitionRepository == null)
                    this._subPropertyDefinitionRepository = new EntityBaseRepository<SubPropertyDefinition>(_context);
                return _subPropertyDefinitionRepository;
            }
        }

        public EntityBaseRepository<FavoriteUser> FavoriteUserRepository
        {
            get
            {
                if (this._favoriteUserRepository == null)
                    this._favoriteUserRepository = new EntityBaseRepository<FavoriteUser>(_context);
                return _favoriteUserRepository;
            }
        }

        public EntityBaseRepository<FavoriteClassified> FavoriteClassifiedRepository
        {
            get
            {
                if (this._favoriteClassifiedRepository == null)
                    this._favoriteClassifiedRepository = new EntityBaseRepository<FavoriteClassified>(_context);
                return _favoriteClassifiedRepository;
            }
        }

        public EntityBaseRepository<Classified> ClassifiedRepository
        {
            get
            {
                if (this._classifiedRepository == null)
                    this._classifiedRepository = new EntityBaseRepository<Classified>(_context);
                return _classifiedRepository;
            }
        }

        public EntityBaseRepository<ClassifiedImage> ClassifiedImageRepository
        {
            get
            {
                if (this._classifiedImageRepository == null)
                    this._classifiedImageRepository = new EntityBaseRepository<ClassifiedImage>(_context);
                return _classifiedImageRepository;
            }
        }

        public EntityBaseRepository<Admin> AdminRepository
        {
            get
            {
                if (this._adminRepository == null)
                    this._adminRepository = new EntityBaseRepository<Admin>(_context);
                return _adminRepository;
            }
        }
        public EntityBaseRepository<ReportUserTicket> ReportUserTicketRepository
        {
            get
            {
                if (this._reportUserTicketRepository == null)
                    this._reportUserTicketRepository = new EntityBaseRepository<ReportUserTicket>(_context);
                return _reportUserTicketRepository;
            }
        }
        public EntityBaseRepository<ReportClassifiedTicket> ReportClassifiedTicketRepository
        {
            get
            {
                if (this._reportClassifiedTicketRepository == null)
                    this._reportClassifiedTicketRepository = new EntityBaseRepository<ReportClassifiedTicket>(_context);
                return _reportClassifiedTicketRepository;
            }
        }
        public EntityBaseRepository<Notification> NotificationRepository
        {
            get
            {
                if (this._notificationRepository == null)
                    this._notificationRepository = new EntityBaseRepository<Notification>(_context);
                return _notificationRepository;
            }
        }
        public EntityBaseRepository<Like> LikeRepository
        {
            get
            {
                if (this._likeRepository == null)
                    this._likeRepository = new EntityBaseRepository<Like>(_context);
                return _likeRepository;
            }
        }
        public EntityBaseRepository<Rate> RateRepository
        {
            get
            {
                if (this._rateRepository == null)
                    this._rateRepository = new EntityBaseRepository<Rate>(_context);
                return _rateRepository;
            }
        }
        public EntityBaseRepository<FeedbackTicket> FeedbackTicketRepository
        {
            get
            {
                if (this._feedbackTicketRepository == null)
                    this._feedbackTicketRepository = new EntityBaseRepository<FeedbackTicket>(_context);
                return _feedbackTicketRepository;
            }
        }
        public EntityBaseRepository<Device> DeviceRepository
        {
            get
            {
                if (this._deviceRepository == null)
                    this._deviceRepository = new EntityBaseRepository<Device>(_context);
                return _deviceRepository;
            }
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
               int x= _context.SaveChanges();
            }
            catch (Exception e)
            {

                //var outputLines = new List<string>();
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                //    }
                //}
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
